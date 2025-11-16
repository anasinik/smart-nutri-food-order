namespace FoodOrderApi.Application.Common.Models.User
{
    public class UploadPhotoResult
    {
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; } = Array.Empty<string>();
        public string? PhotoUrl { get; set; }
    }
}
