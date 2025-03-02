using Amazon.S3;
using Amazon.S3.Model;
using Aqarlist.Core.Models.Database;
using Aqarlist.Core.Services.Service_Interface;
using Microsoft.AspNetCore.Mvc;

namespace Aqarlist.Core.Services.Service_Implementation
{
    public class FileService : IFileService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _configuration;
        private readonly ApiDbContext _db;
        public FileService(IAmazonS3 s3Client, IConfiguration configuration, ApiDbContext db)
        {
            _s3Client = s3Client;
            _configuration = configuration;
            _db = db;
            
        }
        public async Task<int> UploadFileAsync(IFormFile file, int? propertyId = null)
        {
            var attachment = new Attachment() { Name=string.Empty, S3Key= string.Empty };
            try
            {
                var bucketName = _configuration["AWS:BucketName"];
                var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
                var key = $"{Guid.NewGuid().ToString()}/{file.FileName}";
                if (!bucketExists) throw new Exception($"Bucket {bucketName} does not exist.");
                var request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    Key = key,
                    InputStream = file.OpenReadStream(),
                    ContentType = file.ContentType
                };
                await _s3Client.PutObjectAsync(request);
                attachment = new Attachment
                {
                    Name = file.FileName,
                    S3Key = key,
                    CreatedDate = DateTime.UtcNow,
                    FileUrl = string.Empty,
                    PropertyId = propertyId.HasValue ? propertyId.Value : null
                };
                _db.Attachment.Add(attachment);
                _db.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
            return attachment.Id;
        }
        //public async Task<FileResult> GetFileByKeyAsync(int attachmentId)
        //{
        //    var bucketName = _configuration["AWS:BucketName"];
        //    var attachment = _db.Attachment.FirstOrDefault(x => x.Id == attachmentId);
        //    if (attachment is null) throw new Exception();
        //    var s3Object = await _s3Client.GetObjectAsync(bucketName, attachment.S3Key);
        //    return File(s3Object.ResponseStream, s3Object.Headers.ContentType);
        //}
    }
}
