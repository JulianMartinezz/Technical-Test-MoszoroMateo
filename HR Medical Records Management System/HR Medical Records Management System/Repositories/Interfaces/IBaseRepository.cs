namespace HR_Medical_Records_Management_System.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for a base repository with CRUD operations (Create, Read, Update, Delete) 
    /// and filtering capabilities for any entity of type T.
    /// </summary>
    /// <typeparam name="T">The type of entity the repository will handle.</typeparam>
    /// <typeparam name="TKey">The type of the identifier for the entity.</typeparam>
    /// <typeparam name="filters">The type of the filter criteria for retrieving filtered lists.</typeparam>
    public interface IBaseRepository<T, TKey, filters> 
        where T  : class 
        where filters : class
    {
        // <summary>
        /// Adds a new entity to the database.
        /// </summary>
        /// <param name="entity">The entity to be created.</param>
        /// <returns>Returns the created entity after it is saved in the database.</returns>
        /// <exception cref="DbUpdateException">Thrown if an error occurs while saving changes to the database.</exception>
        Task<T> CreateAsync(T entity);


        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <param name="entity">The entity with updated data to be saved.</param>
        /// <returns>Returns the updated entity after saving changes to the database.</returns>
        /// <exception cref="DbUpdateException">Thrown if an error occurs while saving changes to the database.</exception>
        Task<T> UpdateAsync(T entity);


        /// <summary>
        /// Performs a logical or physical deletion of an entity from the database.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        /// <returns>Returns the deleted entity after the operation is performed.</returns>
        /// <exception cref="DbUpdateException">Thrown if an error occurs while saving changes to the database.</exception>
        Task<T> DeleteAsync(T entity);


        /// <summary>
        /// Retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to retrieve.</param>
        /// <returns>Returns the entity with the specified ID, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the provided ID is null.</exception>
        Task<T> GetByIdAsync(TKey id);


        /// <summary>
        /// Retrieves a filtered list of entities based on the provided filter criteria.
        /// </summary>
        /// <param name="filters">An object containing the filter criteria for the query.</param>
        /// <returns>Returns a tuple containing the filtered list of entities and the total count of entities matching the criteria.</returns>
        /// <exception cref="ArgumentException">Thrown if the filters provided are invalid.</exception>
        Task<(List<T>, int totalRows)> GetFilteredListAsync(filters filters);
    }
}
