﻿namespace BuildingBlocks.Pagination;

public class PaginatedRsult<TEntity>
    (int pageIndex, int pageSize, int count, IEnumerable<TEntity> data)
    where TEntity : class
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = pageSize;
    public int Count { get; } = count;
    public IEnumerable<TEntity> Data { get; } = data;
    public int TotalPages => (int)Math.Ceiling(Count / (double)PageSize);
}
