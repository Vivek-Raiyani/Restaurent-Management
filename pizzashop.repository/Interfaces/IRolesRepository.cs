using pizzashop.data.Models;

namespace pizzashop.repository.Interfaces;

public interface IRolesRepository
{
     // retuens the role name
     public string? GetRole(int id);

     // retunes whole row table
     public List<Role> GetAllRoles();

     // returns role is based on name     
     public int GetRoleId(string name);
}
