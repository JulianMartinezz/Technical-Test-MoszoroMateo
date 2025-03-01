using HR_Medical_Records_Management_System.Responses;

namespace HR_Medical_Records_Management_System.Services.Interface
{
    /// <summary>
    /// A generic service interface for handling common CRUD operations with DTOs for creating, updating, deleting,
    /// and querying entities, as well as filtering and returning responses in a consistent format.
    /// </summary>
    /// <typeparam name="T">The type of the entity being handled by the service.</typeparam>
    /// <typeparam name="TKey">The type of the unique identifier of the entity.</typeparam>
    /// <typeparam name="PostDto">The type of the DTO used for creating a new entity.</typeparam>
    /// <typeparam name="UpdateDto">The type of the DTO used for updating an existing entity.</typeparam>
    /// <typeparam name="DeleteDto">The type of the DTO used for deleting an entity.</typeparam>
    /// <typeparam name="filtersDto">The type of the DTO used for filtering the list of entities.</typeparam>
    public interface IBaseService<T, TKey, PostDto, UpdateDto,DeleteDto, filtersDto>
        where T : class
        where PostDto : class
        where UpdateDto : class
        where DeleteDto : class
        where filtersDto : class
    {


        /// <summary>
        /// Asynchronously creates a new entity using the provided DTO for creating the entity.
        /// </summary>
        /// <param name="createDto">The DTO containing the data required to create the new entity.</param>
        /// <returns>A task representing the asynchronous operation, with a BaseResponse containing the created entity.</returns>
        Task<BaseResponse<T>> PostAsync(PostDto createDto);


        /// <summary>
        /// Asynchronously updates an existing entity using the provided DTO for updating the entity.
        /// </summary>
        /// <param name="updateDto">The DTO containing the updated data for the entity.</param>
        /// <returns>A task representing the asynchronous operation, with a BaseResponse containing the updated entity.</returns>
        Task<BaseResponse<T>> PutAsync(UpdateDto updateDto);


        /// <summary>
        /// Asynchronously deletes an entity based on the provided DTO for deleting the entity.
        /// </summary>
        /// <param name="deleteDto">The DTO containing the necessary information to delete the entity.</param>
        /// <returns>A task representing the asynchronous operation, with a BaseResponse indicating the result of the deletion.</returns>
        Task<BaseResponse<T>> DeleteAsync(DeleteDto deleteDto);


        /// <summary>
        /// Asynchronously retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, with a BaseResponse containing the retrieved entity.</returns>
        Task<BaseResponse<T>> GetByIdAsync(TKey id);


        /// <summary>
        /// Asynchronously retrieves a filtered list of entities based on the provided filter criteria.
        /// </summary>
        /// <param name="filtersDto">The DTO containing the filter criteria to query the entities.</param>
        /// <returns>A task representing the asynchronous operation, with a BaseResponse containing a list of filtered entities.</returns>
        Task<BaseResponse<List<T>>> GetFilteredListAsync(filtersDto filtersDto);
    }
}
