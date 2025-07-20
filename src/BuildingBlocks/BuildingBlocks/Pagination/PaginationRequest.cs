﻿namespace BuildingBlocks.Pagination;

public record PaginationRequest(int PageIndex = 0, int PageSize = 10);
//{
//    public int PageIndex { get; } = PageIndex < 1 ? 1 : PageIndex;
//    public int PageSize { get; } = PageSize < 1 ? 10 : PageSize;
//    public int Skip => (PageIndex - 1) * PageSize;
//    public int Take => PageSize;
//}
