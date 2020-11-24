using BlazorInputFile;
using System.Threading.Tasks;

namespace LocalOfferts.Service
{
    public interface IFileUpload
    {
        Task UploadAsync(IFileListEntry fileEntry);
    }
}