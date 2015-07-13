namespace WebAppBootStrap.Control.table.bsTableCore {
    public interface IBaseTable_Page : IBaseTable {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        string PageList { get; set; }
    }
}