using Microsoft.AspNetCore.Mvc;
using pizzashop.services.Interfaces.Orders;
using ClosedXML.Excel;
using static pizzashop.Attributes.CustomAuthorize;
using pizzashop.Constants;
using pizzashop.services.Interfaces;
using Rotativa.AspNetCore;
namespace pizzashop.Controllers;

[ServiceFilter(typeof(PermissionFilter))]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IUploadService _upload;
     private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;
    public OrderController(IOrderService orderService, IUploadService upload, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
    {
        _orderService = orderService;
        _upload = upload;
        hostingEnvironment = env;
    }

    [CustomAuthorize(UserRoles.Admin, UserRoles.Manager)]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult IndexPV()
    {
        return PartialView("IndexPV");
    }

    public IActionResult GetOrderTable(int pageIndex, int pageSize, DateTime startdate, DateTime enddate, string sortname = "", string sorttype = "", int sortbit = 0,
                         string search = "", string status = "allstatus", int time = 1)
    {
        var order = _orderService.Pagination(page: pageIndex, pageSize: pageSize, search: search, status: status, time: time, startdate: startdate, enddate: enddate,
                            sortname: sortname, sorttype: sorttype, sortbit: sortbit);


        ViewBag.status = status;
        ViewBag.time = time;
        ViewBag.startdate = startdate;
        ViewBag.enddate = enddate;
        ViewBag.sortbit = sortbit;

        return PartialView("_OrderTablePV", order);
    }

    public IActionResult OrderDetails(int OrderId)
    {
        var OrderData = _orderService.GetOrderDetail(OrderId);

        return PartialView("_OrderDetailsPV", OrderData);
    }

    public IActionResult ExportExcel(string search, int time, string status = "")
    {
        var ordersdata = _orderService.ExportOrders(search: search, status: status, time: time);

        Guid name = Guid.NewGuid();

        using var wb = new XLWorkbook();
        var ws = wb.AddWorksheet();
        ws.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

        ws.Range("A2", "B3").Merge().Style.Fill.SetBackgroundColor(XLColor.FromHtml("#0066A8"));
        ws.Cell("A2").Value = "Status";
        ws.Cell("A2").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center)
        .Border.SetTopBorder(XLBorderStyleValues.Thin)
        .Border.SetRightBorder(XLBorderStyleValues.Thin)
        .Border.SetBottomBorder(XLBorderStyleValues.Thin)
        .Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range("C2", "F3").Merge();
        ws.Cell("C2").Value = ordersdata.Status;
        ws.Cell("C2").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center)
        .Border.SetTopBorder(XLBorderStyleValues.Thin)
        .Border.SetRightBorder(XLBorderStyleValues.Thin)
        .Border.SetBottomBorder(XLBorderStyleValues.Thin)
        .Border.SetLeftBorder(XLBorderStyleValues.Thin);

        ws.Range("H2", "I3").Merge().Style.Fill.SetBackgroundColor(XLColor.FromHtml("#0066A8"));
        ws.Cell("H2").Value = "Search Text";
        ws.Cell("H2").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center)
        .Border.SetTopBorder(XLBorderStyleValues.Thin)
        .Border.SetRightBorder(XLBorderStyleValues.Thin)
        .Border.SetBottomBorder(XLBorderStyleValues.Thin)
        .Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range("J2", "M3").Merge();
        ws.Cell("J2").Value = ordersdata.Search;
        ws.Cell("J2").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center)
        .Border.SetTopBorder(XLBorderStyleValues.Thin)
        .Border.SetRightBorder(XLBorderStyleValues.Thin)
        .Border.SetBottomBorder(XLBorderStyleValues.Thin)
        .Border.SetLeftBorder(XLBorderStyleValues.Thin);

        ws.Range("A5", "B6").Merge().Style.Fill.SetBackgroundColor(XLColor.FromHtml("#0066A8"));
        ws.Cell("A5").Value = "DATE";
        ws.Cell("A5").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center)
        .Border.SetTopBorder(XLBorderStyleValues.Thin)
        .Border.SetRightBorder(XLBorderStyleValues.Thin)
        .Border.SetBottomBorder(XLBorderStyleValues.Thin)
        .Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range("C5", "F6").Merge();
        ws.Cell("C5").Value = ordersdata.Date;
        ws.Cell("C5").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center)
        .Border.SetTopBorder(XLBorderStyleValues.Thin)
        .Border.SetRightBorder(XLBorderStyleValues.Thin)
        .Border.SetBottomBorder(XLBorderStyleValues.Thin)
        .Border.SetLeftBorder(XLBorderStyleValues.Thin);

        ws.Range("H5", "I6").Merge().Style.Fill.SetBackgroundColor(XLColor.FromHtml("#0066A8"));
        ws.Cell("H5").Value = "No of Record";
        ws.Cell("H5").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center)
        .Border.SetTopBorder(XLBorderStyleValues.Thin)
        .Border.SetRightBorder(XLBorderStyleValues.Thin)
        .Border.SetBottomBorder(XLBorderStyleValues.Thin)
        .Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range("J5", "M6").Merge();
        ws.Cell("J5").Value = ordersdata.Record;
        ws.Cell("J5").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center)
        .Border.SetTopBorder(XLBorderStyleValues.Thin)
        .Border.SetRightBorder(XLBorderStyleValues.Thin)
        .Border.SetBottomBorder(XLBorderStyleValues.Thin)
        .Border.SetLeftBorder(XLBorderStyleValues.Thin);

        var img = "../pizzashop\\wwwroot\\images\\pizzashop_logo.png";
        ws.Range("O2", "P6").Merge();
        ws.AddPicture(img).MoveTo(ws.Cell("O2")).Scale(.3);


        ws.Cell("A9").Value = "Id";
        ws.Cell("A9").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#0066A8"));
        ws.Range("B9", "D9").Merge();
        ws.Cell("B9").Value = "Date";
        ws.Cell("B9").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#0066A8"));
        ws.Range("E9", "G9").Merge();
        ws.Cell("E9").Value = "Customer";
        ws.Cell("E9").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#0066A8"));
        ws.Range("H9", "J9").Merge();
        ws.Cell("H9").Value = "Status";
        ws.Cell("H9").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#0066A8"));
        ws.Range("K9", "L9").Merge();
        ws.Cell("K9").Value = "Payment Mode";
        ws.Cell("K9").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#0066A8"));
        ws.Range("M9", "N9").Merge();
        ws.Cell("M9").Value = "Ratings";
        ws.Cell("M9").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#0066A8"));
        ws.Range("O9", "P9").Merge();
        ws.Cell("O9").Value = "Total Amount";
        ws.Cell("O9").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#0066A8"));

        for (var j = 0; j < ordersdata.OrderData.Count(); j++)
        {
            var i = j + 10;
            ws.Cell("A" + i).Value = ordersdata.OrderData[j].OrderID;
            ws.Range("B" + i, "D" + i).Merge();
            ws.Cell("B" + i).Value = ordersdata.OrderData[j].OrderDate + "";
            ws.Range("E" + i, "G" + i).Merge();
            ws.Cell("E" + i).Value = ordersdata.OrderData[j].Customer;
            ws.Range("H" + i, "J" + i).Merge();
            ws.Cell("H" + i).Value = ordersdata.OrderData[j].Status;
            ws.Range("K" + i, "L" + i).Merge();
            ws.Cell("K" + i).Value = ordersdata.OrderData[j].PaymentMod;
            ws.Range("M" + i, "N" + i).Merge();
            ws.Cell("M" + i).Value = ordersdata.OrderData[j].Rating;
            ws.Range("O" + i, "P" + i).Merge();
            ws.Cell("O" + i).Value = ordersdata.OrderData[j].Total;
        }

        wb.SaveAs("Orders.xlsx");


        var path = Path.GetFullPath("Orders.xlsx");
        byte[] fileBytes = System.IO.File.ReadAllBytes(path);

        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Orders.xlsx");
    }

    public IActionResult OrderInvoice(int OrderId)
    {
        var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        Rotativa.AspNetCore.RotativaConfiguration.Setup(wwwrootPath, "Rotativa");

        var OrderData = _orderService.GetOrderDetail(OrderId);

        if (OrderData == null)
        {
            TempData["Error"] = "Order notfound";
            return RedirectToAction("Index", "Order");
        }

        return new ViewAsPdf("OrderPdf", OrderData) { FileName = "Invoice#" + OrderId + ".pdf" };

        // return View("../Order/OrderPdf",OrderData);
    }

}

