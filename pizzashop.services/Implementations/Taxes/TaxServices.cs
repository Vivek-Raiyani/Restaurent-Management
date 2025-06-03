using pizzashop.data.Models;
using pizzashop.data.ViewModels.Taxes;
using pizzashop.repository.Interfaces.Tax;
using pizzashop.services.Interfaces.Taxes;

namespace pizzashop.services.Implementations.Taxes;

public class TaxServices : ITaxServices
{

     private readonly ITaxRepository _taxRepo;

    public TaxServices(ITaxRepository taxRepo){
        _taxRepo = taxRepo;
    }

    #region  read
        public IEnumerable<TaxesListVM> GetAllTaxes(string search){
            var taxes = _taxRepo.ReadAll(search);
            var taxesList =  taxes.Select(t => new TaxesListVM{
                TaxId = t.TaxId,
                TaxName = t.TaxName,
                TaxType = t.TaxType,
                TaxValue = (float)t.TaxAmount,
                IsEnabled = (bool)t.IsEnabled, 
                IsDefault = (bool)t.IsDefault
            });
            return taxesList;
        }
        public TaxesListVM GetTaxById(int taxid){
            var taxes = _taxRepo.Read(taxid);
            var taxvm =  new TaxesListVM();
                taxvm.TaxId = taxes.TaxId;
                taxvm.TaxName = taxes.TaxName;
                taxvm.TaxType = taxes.TaxType;
                taxvm.TaxValue = (float)taxes.TaxAmount;
                taxvm.IsEnabled = (bool)taxes.IsEnabled; 
                taxvm.IsDefault = (bool)taxes.IsDefault;
            return taxvm;
        }

    #endregion

    #region  add
        public bool AddTax(TaxesListVM taxes){
            var tax =  new Taxis();
            tax.TaxName = taxes.TaxName;
            tax.TaxType = taxes.TaxType;
            tax.TaxAmount = taxes.TaxValue;
            tax.IsEnabled = taxes.IsEnabled;
            tax.IsDefault = taxes.IsDefault;
            return _taxRepo.Add(tax);
        }
    #endregion

    #region  edit
        public bool EditTax(TaxesListVM taxes){
            Taxis tax = new(){
                TaxId = taxes.TaxId,
                TaxName = taxes.TaxName,
                TaxAmount = taxes.TaxValue,
                IsDefault = taxes.IsDefault,
                IsEnabled = taxes.IsEnabled,
                TaxType = taxes.TaxType,
            };
            return _taxRepo.Update(tax);
        }
    #endregion

    #region  delete
        public bool DeleteTax(int taxid){
            Taxis tax = _taxRepo.Read(taxid);
            tax.IsDeleted = true;
            return _taxRepo.Update(tax);
        }
    #endregion

    #region  check constrain

    public bool CheckConstrain( string value ){
        return _taxRepo.ReadAll().FirstOrDefault( o=> o.TaxName == value) == null ;
    }

    #endregion
}
