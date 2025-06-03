using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using pizzashop.Constants;
using pizzashop.services.Interfaces.Customers;
using static pizzashop.Attributes.CustomAuthorize;

namespace pizzashop.Controllers;

[ServiceFilter(typeof(PermissionFilter))]
public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [CustomAuthorize(UserRoles.Admin, UserRoles.Manager)]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetCustomerTable(int pageIndex, DateTime StartDate, DateTime Enddate, int pageSize = 3,
                                int status = 1, string search = "", string sortname = "", string sorttype = "", int sortbit = 0)
    {
        var customers = _customerService.PaginationCustomer(search: search, page: pageIndex, pageSize: pageSize, status: status, StartDate: StartDate, Enddate: Enddate,
                        sortname: sortname, sorttype: sorttype, sortbit: sortbit);

        ViewBag.status = status;
        ViewBag.sortbit = sortbit;

        return PartialView("_CustomerTablePV", customers);
    }

    public IActionResult GetCustomerDetails(int CustomerId)
    {
        var history = _customerService.CustomerDetails(CustomerId);

        return PartialView("_CustomerPV", history);
    }

    public IActionResult ExportExcel(string search, int status, DateTime start, DateTime end)
    {
        var Customerdata = _customerService.GetExportCustomer(search: search, status: status, from: start, to: end);

        Guid name = Guid.NewGuid();

        using var wb = new XLWorkbook();
        var ws = wb.AddWorksheet();
        ws.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

        ws.Range("A2", "B3").Merge().Style.Fill.SetBackgroundColor(XLColor.FromHtml("#0066A8"));
        ws.Cell("A2").Value = "Account";
        ws.Cell("A2").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center)
        .Border.SetTopBorder(XLBorderStyleValues.Thin)
        .Border.SetRightBorder(XLBorderStyleValues.Thin)
        .Border.SetBottomBorder(XLBorderStyleValues.Thin)
        .Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range("C2", "F3").Merge();
        ws.Cell("C2").Value = Customerdata.Status;
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
        ws.Cell("J2").Value = Customerdata.Search;
        ws.Cell("J2").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center)
        .Border.SetTopBorder(XLBorderStyleValues.Thin)
        .Border.SetRightBorder(XLBorderStyleValues.Thin)
        .Border.SetBottomBorder(XLBorderStyleValues.Thin)
        .Border.SetLeftBorder(XLBorderStyleValues.Thin);

        ws.Range("A5", "B6").Merge().Style.Fill.SetBackgroundColor(XLColor.FromHtml("#0066A8"));
        ws.Cell("A5").Value = "Date";
        ws.Cell("A5").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center)
        .Border.SetTopBorder(XLBorderStyleValues.Thin)
        .Border.SetRightBorder(XLBorderStyleValues.Thin)
        .Border.SetBottomBorder(XLBorderStyleValues.Thin)
        .Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range("C5", "F6").Merge();
        ws.Cell("C5").Value = Customerdata.Date;
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
        ws.Cell("J5").Value = Customerdata.Record;
        ws.Cell("J5").Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center)
        .Border.SetTopBorder(XLBorderStyleValues.Thin)
        .Border.SetRightBorder(XLBorderStyleValues.Thin)
        .Border.SetBottomBorder(XLBorderStyleValues.Thin)
        .Border.SetLeftBorder(XLBorderStyleValues.Thin);
        // "D:\Projects\pizzashop curent\PizzaShop_NTier\pizzashop\wwwroot\images\pizzashop_logo.png"
        var img = "../pizzashop\\wwwroot\\images\\pizzashop_logo.png";
        ws.Range("O2", "P6").Merge();
        ws.AddPicture(img).MoveTo(ws.Cell("O2")).Scale(.3);


        ws.Cell("A9").Value = "Id";
        ws.Cell("A9").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#0066A8"));
        ws.Range("B9", "D9").Merge();
        ws.Cell("B9").Value = "Name";
        ws.Cell("B9").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#0066A8"));
        ws.Range("E9", "H9").Merge();
        ws.Cell("E9").Value = "Email";
        ws.Cell("E9").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#0066A8"));
        ws.Range("I9", "K9").Merge();
        ws.Cell("I9").Value = "Date";
        ws.Cell("I9").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#0066A8"));
        ws.Range("L9", "N9").Merge();
        ws.Cell("L9").Value = "Mobile Number";
        ws.Cell("L9").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#0066A8"));
        ws.Range("O9", "P9").Merge();
        ws.Cell("O9").Value = "Total Order";
        ws.Cell("O9").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#0066A8"));


        for (var j = 0; j < Customerdata.Customer.Count(); j++)
        {
            var i = j + 10;
            ws.Cell("A" + i).Value = Customerdata.Customer[j].CustomerId;
            ws.Range("B" + i, "D" + i).Merge();
            ws.Cell("B" + i).Value = Customerdata.Customer[j].Name;
            ws.Range("E" + i, "H" + i).Merge();
            ws.Cell("E" + i).Value = Customerdata.Customer[j].Email;
            ws.Range("I" + i, "K" + i).Merge();
            ws.Cell("I" + i).Value = Customerdata.Customer[j].CreatedOn;
            ws.Range("L" + i, "N" + i).Merge();
            ws.Cell("L" + i).Value = Customerdata.Customer[j].PhoneNo;
            ws.Range("O" + i, "P" + i).Merge();
            ws.Cell("O" + i).Value = Customerdata.Customer[j].TotalOrders;
        }

        wb.SaveAs("Customers.xlsx");

        var path = Path.GetFullPath("Customers.xlsx");
        byte[] fileBytes = System.IO.File.ReadAllBytes(path);

        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Customers.xlsx");
    }



}
