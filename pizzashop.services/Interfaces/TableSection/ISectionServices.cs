using pizzashop.data.Models;
using pizzashop.data.ViewModels;
using pizzashop.data.ViewModels.TableSection;

namespace pizzashop.services.Interfaces.TableSection;

public interface ISectionServices
{

    public IEnumerable<SectionSidebarVM> GetAllSections();

    public bool AddNewSection(SectionSidebarVM section);

    public bool UpdateSection(SectionSidebarVM section);

    public bool DeleteSection(int SectionId);

    public SectionSidebarVM  GetSectionById(int SectionId);

    // public List<TableVM> GetSectionTables(int SectionId)
    public PaginatedListVM<TableVM> GetSectionTables(int SectionId,int page, int pageSize, string search);

    public bool CheckConstrain( string value );
    
}
