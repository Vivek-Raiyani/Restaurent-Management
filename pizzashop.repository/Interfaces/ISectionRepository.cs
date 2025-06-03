using pizzashop.data.Models;
using pizzashop.data.ViewModels.TableSection;

namespace pizzashop.repository.Interfaces.TableSection;

public interface ISectionRepository
{
    public List<TableDetail> PaginationTable(int page, int pageSize, string search, int SectionId);
    
    public int PaginationTableCount(string search,int SectionId);

    public bool Add(Section sec);
    public bool Update(Section sec);
    public bool UpdateMany(List<Section> sections);
    public Section Read(int sectionid);
    public IEnumerable<Section> ReadAll();
}
