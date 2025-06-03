

namespace pizzashop.data.ViewModels;

public class DashboardVM
{
    public float TotalSales { get; set; }

    public int OrderCount { get; set; }

    public float AvgOrderValue { get; set; }

    public TimeSpan AvgOrderWait { get; set; }

    public int NewCustomerCount { get; set; }



    public List<DashboardItemVM> TopSellingItem { get; set; } = new List<DashboardItemVM>();

    public List<DashboardItemVM> LeastSellingItem { get; set; } = new List<DashboardItemVM>();

}

 public class DashboardChartJson
{
    public List<DashboardChartVM> Revenue { get; set; } = new List<DashboardChartVM>();

    public List<DashboardChartVM> Customer { get; set; } = new List<DashboardChartVM>();

}

