﻿namespace Undersoft.SDK.Service.Application.GUI.Models;

public class Pagination
{
    public bool HasNextPage => PageIndex - IndexFrom + 1 < TotalPages;

    public bool HasPreviousPage => PageIndex - IndexFrom > 0;

    public int PagingLimit { get; set; } = 11;

    public int PagingRange { get; set; } = 5;

    public int IndexFrom { get; set; } = 1;

    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 20;

    public int Offset => (PageIndex - IndexFrom) * PageSize;

    public int TotalCount { get; set; } = -1;

    public bool CountChanged { get; set; } = false;

    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    public void SetPageIndex(int pageIndex)
    {
        PageIndex = pageIndex;
    }

    public void SetTotalCount(int totalCount)
    {
        if (totalCount == TotalCount)
        {
            return;
        }

        TotalCount = totalCount;

        if (PageIndex - IndexFrom > 0 && PageIndex > TotalPages)
        {
            SetPageIndex(TotalPages);
        }
    }

    public void SetPagingLimit(int pagingLimit)
    {
        PagingLimit = pagingLimit;
        if (PagingLimit % 2 == 0)
            PagingLimit += 1;
        PagingRange = PagingLimit / 2;
    }

    public int GetLowestPageIndex()
    {
        var lowest = PageIndex / PagingRange;
        if (lowest == 0)
            return 1;
        return (lowest - 1) * PagingRange + (lowest * PagingRange - PageIndex);
    }

    public int GetHighestPageIndex()
    {
        var highest = GetLowestPageIndex() + PagingLimit - 1;
        if (highest > TotalPages)
            return TotalPages;
        return highest;
    }
}