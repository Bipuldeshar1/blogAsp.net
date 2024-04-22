
namespace blogg.service
{
    public interface IImageService
    {
        Task<string> uploadImageAsync(IFormFile file);
    }
}