using HR_Medical_Records_Management_System.Responses;

namespace HR_Medical_Records_Management_System.Services.Interface
{
    public interface IBaseService<BaseResponse, TKey, PostDto, UpdateDto,DeleteDto>
        where BaseResponse : class
        where PostDto : class
        where UpdateDto : class
        where DeleteDto : class
    {
        Task<BaseResponse> PostAsync(PostDto createDto);
        Task<BaseResponse> PutAsync(UpdateDto updateDto);
        Task<BaseResponse> DeleteAsync(DeleteDto deleteDto);
        Task<BaseResponse> GetByIdAsync(TKey id);
        Task<List<BaseResponse>> GetListAsync();
    }
}
