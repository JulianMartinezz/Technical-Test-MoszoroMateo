using HR_Medical_Records_Management_System.Responses;

namespace HR_Medical_Records_Management_System.Services.Interface
{
    public interface IBaseService<T, TKey, PostDto, UpdateDto,DeleteDto, filtersDto>
        where T : class
        where PostDto : class
        where UpdateDto : class
        where DeleteDto : class
        where filtersDto : class
    {
        Task<BaseResponse<T>> PostAsync(PostDto createDto);
        Task<BaseResponse<T>> PutAsync(UpdateDto updateDto);
        Task<BaseResponse<T>> DeleteAsync(DeleteDto deleteDto);
        Task<BaseResponse<T>> GetByIdAsync(TKey id);
        Task<BaseResponse<List<T>>> GetFilteredListAsync(filtersDto filtersDto);
    }
}
