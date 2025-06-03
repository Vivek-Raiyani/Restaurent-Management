using Microsoft.AspNetCore.Mvc;
using pizzashop.data.Models;
using pizzashop.data.ViewModels.OrderApp.Tables;
using pizzashop.data.ViewModels.OrderApp.Waiting;
using pizzashop.repository.Interfaces.Customers;
using pizzashop.repository.Interfaces.OrderApp;
using pizzashop.repository.Interfaces.TableSection;
using pizzashop.services.Interfaces.OrderApp;

namespace pizzashop.services.Implementations.OrderApp;

public class WaitingListServices : IWaitingListServices
{
    private readonly IWaitingListRepository _wl;

    private readonly ISectionRepository _section;

    private readonly ITableRepository _table;

    private readonly ICustomerRepository _customer;

    public WaitingListServices(IWaitingListRepository wl, ISectionRepository section , ITableRepository table , ICustomerRepository customer )
    {
        _wl = wl;
        _section = section;
        _table = table;
        _customer = customer;
    }

    public IEnumerable<OrderSectionVM> GetSections(int floorid)
    {
        if (floorid != 0)
        {
            var sections = _section.Read(floorid);
            var sectionVMs = new List<OrderSectionVM>();
            var sectionVM = new OrderSectionVM();
            sectionVM.Id = sections.SectionId;
            sectionVM.Name = sections.SecName;
            sectionVMs.Add(sectionVM);
            return sectionVMs;
        }
        else
        {
            return _section.ReadAll().Select(s => new OrderSectionVM()
            {
                Id = s.SectionId,
                Name = s.SecName,
            });
        }
    }

     public IEnumerable<TableVM> GetAvailableTables(int floorid)
    {
            return _table.GetSectionTableAll(floorid).Where(t=>t.TableStatus =="available").Select(s => new TableVM()
            {
                Id = s.TableId,
                Name = s.TblName,
            }).ToList();
    }
    

    public bool UpdateWaitingList(WaitingTokenVM waitingToken)
    {
        List<string> customerEmails = _customer.ReadOrderProgress().Select(c=> c.Email).ToList();
        // List<string> tokenEmails = _wl.GetAllWaitingList().Select(c=> c.CustEmail).ToList();
        
        if(customerEmails.Contains(waitingToken.Email) )
        {
            return false;
        }

        var token = new Waitlist()
        {
            TokenId = waitingToken.TokenId,
            CustName = waitingToken.Name,
            CustEmail = waitingToken.Email,
            Phone = waitingToken.Phone,
            NoPeople = (short)waitingToken.Persons,
            SectionsId = waitingToken.Sectionid,
        };
        var result = _wl.UpdateWaitingList(token);
        if (result)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public IEnumerable<WaitingTokenVM> GetWaitingList(int floorid)
    {
        return _wl.GetAllWaitingList(floorid).Select(s => new WaitingTokenVM()
        {
            TokenId = s.TokenId,
            Name = s.CustName,
            Email = s.CustEmail,
            Phone = s.Phone,
            Persons = (int)s.NoPeople,
            Sectionid = (int)s.SectionsId
        });
    }

    public WaitingTokenVM GetWaitingToken(int tokenid)
    {
        var token = _wl.WaitlistToken(tokenid);
        var tokenvm = new WaitingTokenVM(){
            TokenId = token.TokenId,
            Name = token.CustName ?? "",
            Email = token.CustEmail ?? "",
            Phone = token.Phone ?? "",
            Persons = (int)token.NoPeople,
            Sectionid = (int)token.SectionsId
        };

        return tokenvm;
    }

    public IEnumerable<WaitingTokenTable> GetWaitingTable(int floorid){
         return _wl.GetAllWaitingList(floorid).Select(s => new WaitingTokenTable()
        {
            Id = s.TokenId,
            Name = s.CustName,
            Email = s.CustEmail,
            Phone = s.Phone,
            Persons = s.NoPeople,
            Sectionid = s.SectionsId,
            CreatedAt = (DateTime)s.CreatedOn
        });
    }

    public bool Delete(int tokenid)
    {
        var token = _wl.WaitlistToken(tokenid);
        return _wl.DeleteWaitingList(token);
        
    }

    public EmailCustomer GetEmailCustomer(string email)
    {   
        Customer data =  _customer.ExisitingCustomerCheck(email : email);
        if(data == null ){
            return new EmailCustomer();
        }
        EmailCustomer customer = new(){
            Email = email,
            Phone =  data.PhoneNo,
            Name = data.Name,
        };

        return customer;
    }

    public int WaitinfTokenCount()
    {
        return _wl.GetAllWaitingList().Count();
    }
}
