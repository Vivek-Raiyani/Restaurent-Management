using pizzashop.data.Models;
using pizzashop.data.ViewModels;
using pizzashop.data.ViewModels.TableSection;
using pizzashop.repository.Interfaces.TableSection;
using pizzashop.services.Interfaces.TableSection;

namespace pizzashop.services.Implementations.TableSection;

public class SectionServices : ISectionServices
{

    private readonly ISectionRepository _sectionRepo;

    public SectionServices(ISectionRepository sectionRepo){
        _sectionRepo = sectionRepo;
    }

    public IEnumerable<SectionSidebarVM> GetAllSections(){
        var sections = _sectionRepo.ReadAll();
        var SectionList = sections.Select(s => new SectionSidebarVM(){
            SectionId = s.SectionId,
            Description = s.Description,
            SectionName = s.SecName
        });
        return SectionList;
    }

    public SectionSidebarVM  GetSectionById(int SectionId){
        var section = new SectionSidebarVM();
        var data = _sectionRepo.Read(SectionId);
        section.SectionId = SectionId;
        section.SectionName = data.SecName;
        section.Description = data.Description;
        return section;
    }

    public bool AddNewSection(SectionSidebarVM section){
        Section dbSection = new(){
            SecName = section.SectionName,
            Description = section.Description,
        };
        var result = _sectionRepo.Add(dbSection);
        return result;
    }

    public bool UpdateSection(SectionSidebarVM section){
        Section sec = new(){
            SectionId = section.SectionId,
            SecName = section.SectionName,
            Description = section.Description
        };
        var result = _sectionRepo.Update(sec);
        return result;
    }

    public bool DeleteSection(int SectionId){
        Section sec = _sectionRepo.Read(SectionId);
        sec.IsDeleted = true;
        var result = _sectionRepo.Update(sec);
        return result;
    }

    public PaginatedListVM<TableVM> GetSectionTables(int SectionId,int page, int pageSize, string search){
        var count = 0;

        count = _sectionRepo.PaginationTableCount(search, SectionId);
        var tables = _sectionRepo.PaginationTable(page, pageSize, search, SectionId);

        // need to fix it
        List<TableVM> Sectiontables = new List<TableVM>();
        foreach (var item in tables)
        {
            var element = new TableVM();
            element.SectionId = SectionId;
            element.TableName = item.TblName;
            element.TableId = item.TableId;
            element.Capacity = (short)item.Capacity;
            element.Status = item.TableStatus;
            Sectiontables.Add(element);
        }
        int totalPages = (int)Math.Ceiling(count / (double)pageSize);

        return new PaginatedListVM<TableVM>(Sectiontables, page, totalPages, pageSize,search);
        
    }

     #region  check constrain

    public bool CheckConstrain( string value )
    {
        return _sectionRepo.ReadAll().FirstOrDefault( o=> o.SecName == value) == null ;
    }

    #endregion
    

}
