using pizzashop.data.Models;
using pizzashop.data.ViewModels;
using pizzashop.repository.Interfaces;
using pizzashop.services.Interfaces;

namespace pizzashop.services.Implementations;

public class MenuService : IMenuService
{

    private readonly ICategoryRepository _categoryRepo;
    private readonly IModifierGroupRepository _modifierGroupRepo;
    private readonly IItemModifierGroupMappingRepository _itemModGroup;
    private readonly IModifiersRepository _modifiersRepo;
    private readonly IItemRepository _itemRepository;

    private readonly IModifierGroupMappingRepository _modifierGroupMappingRepo;

    public MenuService(ICategoryRepository menuService, IModifierGroupRepository _modifer, IItemRepository itemRepository,
     IModifiersRepository modifiersRepo, IItemModifierGroupMappingRepository itemModGroup, IModifierGroupMappingRepository modifierGroupMappingRepository)
    {
        _categoryRepo = menuService;
        _modifierGroupRepo = _modifer;
        _itemRepository = itemRepository;
        _modifiersRepo = modifiersRepo;
        _itemModGroup = itemModGroup;
        _modifierGroupMappingRepo = modifierGroupMappingRepository;
    }

    public IEnumerable<CategoryVM> CategorySidebar()
    {
        var dbcategory = _categoryRepo.ReadAll();
        var categories = dbcategory.Select(c => new CategoryVM()
        {
            CategoryID = c.CategoryId,
            CategoryName = c.MenuName,
            CategoryDescription = c.MenuDescription
        });
        return categories;
    }

    public IEnumerable<ItemCategory> CategoryList()
    {
        var dbcategory = _categoryRepo.ReadAll();
        var categories = dbcategory.Select(c => new ItemCategory()
        {
            Id = c.CategoryId,
            Name = c.MenuName,
        });
        return categories;
    }


    public IEnumerable<ModifierGroupNameVM> ModifierSidebar()
    {
        var dbgroups = _modifierGroupRepo.ReadAll();
        var groups = dbgroups.Select(c => new ModifierGroupNameVM()
        {
            GroupId = c.ModGroupId,
            GroupName = c.ModifierName,
            ModiferCount = c.ModifierGroupMappings.Count
        });
        return groups;
    }

    public ItemListVM ItemTable(int categoryId, int page, int pagesize, string search)
    {
        var dbitems = _itemRepository.ReadAll(categoryid: categoryId, search: search);
        var totalItems = dbitems.Count();
        int totalPages = (int)Math.Ceiling(totalItems / (double)pagesize);

        dbitems = dbitems.Skip((page - 1) * pagesize).Take(pagesize);

        var items = dbitems.Select(s => new ItemtableVM()
        {
            Name = s.IteamName,
            Id = s.IteamId,
            Rate = s.Rate,
            ItemType = s.ItemType,
            Image = s.IteamImg,
            Quantity = s.Quantity,
            Available = s.Available
        }).ToList();

        var pagination = new PaginatedListVM<ItemtableVM>(items: items, pageIndex: page, totalPages: totalPages, pagesize: pagesize, search: search, totalItems: totalItems);
        return new ItemListVM()
        {
            CategoryId = categoryId,
            CatItem = pagination
        };
    }


    public ModifierTableVM ModifierTable(int GroupId, int page, int pagesize, string search)
    {
        var dbitems = new List<Modifier>();
        if (GroupId == 0)
        {
            dbitems = _modifiersRepo.ReadAll(search: search).ToList();
        }
        else
        {
            dbitems = _modifiersRepo.ReadGroup(groupid: GroupId, search: search).ToList();
        }
        var totalItems = dbitems.Count();
        // var totalPages = totalItems / pagesize;
        int totalPages = (int)Math.Ceiling(totalItems / (double)pagesize);
        dbitems = dbitems.Skip((page - 1) * pagesize).Take(pagesize).ToList();

        var items = dbitems.Select(s => new ModifierItemVM()
        {
            ModifierId = s.ModifierId,
            ModName = s.ModName,
            Unit = s.Unit,
            Rate = s.Rate,
            Quantity = s.Quantity,
        }).ToList();

        var pagination = new PaginatedListVM<ModifierItemVM>(items: items, pageIndex: page, totalPages: totalPages, pagesize: pagesize, search: search, totalItems: totalItems);
        return new ModifierTableVM()
        {
            ModiferGroupId = GroupId,
            Modifiers = pagination
        };
    }

    public ModifierTableVM ModifierTableModal(int GroupId, int page, int pagesize, string search, List<ModiferModalVM> modifIders)
    {
        var dbitems = new List<Modifier>();
        if (GroupId == 0)
        {
            dbitems = _modifiersRepo.ReadAll(search: search).ToList();
        }
        else
        {
            dbitems = _modifiersRepo.ReadGroup(groupid: GroupId, search: search).ToList();
        }
        var totalItems = dbitems.Count();
        // var totalPages = totalItems / pagesize;
        int totalPages = (int)Math.Ceiling(totalItems / (double)pagesize);
        dbitems = dbitems.Skip((page - 1) * pagesize).Take(pagesize).ToList();

        var items = dbitems.Select(s => new ModifierItemVM()
        {
            ModifierId = s.ModifierId,
            ModName = s.ModName,
            Unit = s.Unit,
            Rate = s.Rate,
            Quantity = s.Quantity,
            MappingId = modifIders.Where(t => t.Id == s.ModifierId).Select(x => x.MappingId).Count() == 0 ? 0 : modifIders.Where(t => t.Id == s.ModifierId).Select(x => x.MappingId).First()
        }).ToList();

        var pagination = new PaginatedListVM<ModifierItemVM>(items: items, pageIndex: page, totalPages: totalPages, pagesize: pagesize, search: search, totalItems: totalItems);
        return new ModifierTableVM()
        {
            ModiferGroupId = GroupId,
            Modifiers = pagination
        };
    }

    public CategoryVM ReadCategory(int categoryId)
    {
        var category = _categoryRepo.Read(categoryid: categoryId);
        if(category.CategoryId == 0){
            return new CategoryVM();
        }
        return new CategoryVM()
        {
            CategoryID = categoryId,
            CategoryName = category.MenuName,
            CategoryDescription = category.MenuDescription,
        };
    }

    public AddEditItemVM ReadItem(int itemId)
    {
        var dbitem = _itemRepository.Read(itemid: itemId);
        var item = new AddEditItemVM()
        {
            Name = dbitem.IteamName,
            ItemId = itemId,
            Type = dbitem.ItemType,
            CategoryId = dbitem.CategoryId,
            CategoryName = dbitem.Category.MenuName,
            Rate = dbitem.Rate,
            Quantity = dbitem.Quantity,
            TaxPercentage = dbitem.TaxPercentage,
            ShortCode = dbitem.ShortCode,
            DefaultTax = (bool)dbitem.DefaultTax,
            Description = dbitem.Description,
            Img = dbitem.IteamImg,
            Available = (bool)dbitem.Available,
            Unit = dbitem.Unit,
        };
        // item.categories = _categoryRepo.ReadAll().Select(c => new ItemCategory(){
        //     Id = c.CategoryId,
        //     Name = c.MenuName,
        // }).ToList();
        return item;
    }

    public AddEditModifier ReadModifier(int modifierId)
    {
        var dbmodifier = _modifiersRepo.Read(modifierid: modifierId);
        var modifier = new AddEditModifier()
        {
            ModiferId = modifierId,
            Name = dbmodifier.ModName,
            Rate = dbmodifier.Rate,
            Description = dbmodifier.ModifierDesc,
            Quantity = dbmodifier.Quantity,
            Unit = dbmodifier.Unit,
            GroupIds = dbmodifier.ModifierGroupMappings.Select(m => m.ModifierGroupId).ToList(),
        };
        modifier.Groups = _modifierGroupRepo.ReadAll().Select(s => new ModifierVM()
        {
            ModGroupId = s.ModGroupId,
            ModifierName = s.ModifierName,
        }).ToList();
        return modifier;
    }

    public List<ItemGroupData> ReadItemMappings(int itemId)
    {
        var dbmappings = _itemModGroup.ReadItemGroups(itemid: itemId);
        var mappings = dbmappings.Select(m => new ItemGroupData()
        {
            MappingId = m.ItemModId,
            GroupId = m.ModiferId,
            Name = m.Modifer.ModifierName,
            Max = m.Maximum,
            Min = m.Minimun,
            Modifiers = m.Modifer.ModifierGroupMappings.Select(m => new ItemModifierDisplay()
            {
                Name = m.Modifier.ModName,
                Rate = (float)m.Modifier.Rate
            }).ToList()
        });
        return mappings.ToList();
    }

    public ItemGroupData ShowMapping(int GroupId)
    {
        var mapping = _modifierGroupRepo.Read(GroupId);
        var group = new ItemGroupData()
        {
            GroupId = mapping.ModGroupId,
            Name = mapping.ModifierName,
            Modifiers = mapping.ModifierGroupMappings.Select(s => new ItemModifierDisplay()
            {
                Name = s.Modifier.ModName,
                Rate = (float)s.Modifier.Rate
            }).ToList(),
        };
        return group;
    }
    public AddEditGroup ReadGroup(int GroupId)
    {
        var dbgroup = _modifierGroupRepo.Read(groupid: GroupId);
        var group = new AddEditGroup()
        {
            Id = dbgroup.ModGroupId,
            Name = dbgroup.ModifierName,
            Description = dbgroup.GroupDescription,
            Modifier = dbgroup.ModifierGroupMappings.Where(m => m.Isdeleted != true).Select(s => new ModifierNameVM()
            {
                MappingId = s.MappingId,
                Name = s.Modifier.ModName,
                Id = s.Modifier.ModifierId,
            }).ToList(),
        };

        return group;
    }



    // update 

    public bool AddEditCategory(CategoryVM category)
    {
        bool result = false;
        if (category.CategoryID == 0)
        {
            var categoryobj = new MenuCategory()
            {
                MenuName = category.CategoryName,
                MenuDescription = category.CategoryDescription
            };
            result = _categoryRepo.Add(categoryobj);
        }
        else
        {
            var dbcategory = _categoryRepo.Read(category.CategoryID);
            if(dbcategory.CategoryId == 0){
                return false;
            }
            dbcategory.MenuName = category.CategoryName;
            dbcategory.MenuDescription = category.CategoryDescription;
            result = _categoryRepo.Update(dbcategory);
        }
        return result;
    }

    public bool AddEditItem(AddEditItemVM item)
    {
        bool result = false;
        if (item.ItemId == 0)
        {
            var itemobj = new MenuItem()
            {
                CategoryId = item.CategoryId,
                IteamName = item.Name,
                ItemType = item.Type,
                Rate = item.Rate,
                Quantity = item.Quantity,
                Unit = item.Unit,
                Available = item.Available,
                DefaultTax = item.DefaultTax,
                TaxPercentage = item.TaxPercentage,
                ShortCode = item.ShortCode,
                Description = item.Description,
                IteamImg = item.Img,
            };
            itemobj.ItemModifierGroups = item.Groups.Select(s => new ItemModifierGroup()
            {
                ModiferId = s.GroupId,
                Minimun = s.Min,
                Maximum = s.Max,
            }).ToList();
            result = _itemRepository.Add(itemobj);
        }
        else
        {
            var dbitem = _itemRepository.Read(item.ItemId);
            dbitem.CategoryId = item.CategoryId;
            dbitem.IteamName = item.Name;
            dbitem.ItemType = item.Type;
            dbitem.Rate = item.Rate;
            dbitem.Quantity = item.Quantity;
            dbitem.Unit = item.Unit;
            dbitem.Available = item.Available;
            dbitem.DefaultTax = item.DefaultTax;
            dbitem.TaxPercentage = item.TaxPercentage;
            dbitem.ShortCode = item.ShortCode;
            dbitem.Description = item.Description;
            dbitem.IteamImg = item.Img == null ? dbitem.IteamImg : item.Img;

            var dbmappings = _itemModGroup.ReadItemGroups(itemid: item.ItemId);
            var newmappings = item.Groups.Select(s => s.Mappingid);

            dbitem.ItemModifierGroups = new List<ItemModifierGroup>();
            foreach (var map in item.Groups.Where(s => s.Mappingid == 0))
            {
                var mappingobj = new ItemModifierGroup();
                mappingobj.ModiferId = map.GroupId;
                mappingobj.Minimun = map.Min;
                mappingobj.Maximum = map.Max;
                dbitem.ItemModifierGroups.Add(mappingobj);
            };
            foreach (var oldmap in dbmappings)
            {
                if (!newmappings.Contains(oldmap.ItemModId))
                {
                    oldmap.Isdeleted = true;
                }
                dbitem.ItemModifierGroups.Add(oldmap);
            }

            result = _itemRepository.Update(dbitem);
        }
        return result;
    }

    // public bool AddEditGrop(){}

    public bool AddEditModifier(AddEditModifier modifier)
    {
        bool result = false;
        if (modifier.ModiferId == 0)
        {
            // add new modifier 
            Modifier dbModifier = new Modifier()
            {
                ModName = modifier.Name,
                ModifierDesc = modifier.Description,
                Rate = modifier.Rate,
                Quantity = modifier.Quantity,
                Unit = modifier.Unit,
                ModifierGroupMappings = modifier.GroupIds.Select(s => new ModifierGroupMapping
                {
                    ModifierGroupId = s
                }).ToList()
            };
            result = _modifiersRepo.AddUpdate(dbModifier);
        }
        else
        {
            // edit new modifier
            Modifier dbModifier = _modifiersRepo.Read(modifier.ModiferId);
            if (dbModifier != null)
            {
                dbModifier.ModName = modifier.Name;
                dbModifier.ModifierDesc = modifier.Description;
                dbModifier.Rate = modifier.Rate;
                dbModifier.Quantity = modifier.Quantity;
                dbModifier.Unit = modifier.Unit;

                var oldmapping = _modifierGroupMappingRepo.ReadModifierGroups(modifier.ModiferId);
                foreach (var map in oldmapping)
                {
                    if (modifier.GroupIds.Contains(map.ModifierGroupId))
                    {
                        dbModifier.ModifierGroupMappings.Add(map);
                        modifier.GroupIds.Remove(map.ModifierGroupId);
                    }
                    else
                    {
                        map.Isdeleted = true;
                        dbModifier.ModifierGroupMappings.Add(map);
                    }
                }

                foreach (var id in modifier.GroupIds)
                {
                    var newmappings = new ModifierGroupMapping
                    {
                        ModifierGroupId = id,
                        ModifierId = modifier.ModiferId // not needed   
                    };
                    dbModifier.ModifierGroupMappings.Add(newmappings);
                }

                result = _modifiersRepo.AddUpdate(dbModifier);

            }
        }
        return result;
    }

    public bool AddEditGroup(AddEditGroup group)
    {
        bool result = false;
        if (group.Id == 0)
        {
            // add
            ModifierGroup dbGroup = new ModifierGroup()
            {
                ModifierName = group.Name,
                GroupDescription = group.Description,
                ModifierGroupMappings = group.Modifier.Select(s => new ModifierGroupMapping()
                {
                    ModifierId = s.Id
                }).ToList()
            };

            result = _modifierGroupRepo.Add(group: dbGroup);

        }
        else
        {
            // edit
            ModifierGroup dbGroup = _modifierGroupRepo.Read(groupid: group.Id);
            if (dbGroup != null)
            {
                dbGroup.ModifierName = group.Name;
                dbGroup.GroupDescription = group.Description;

                var oldmapping = dbGroup.ModifierGroupMappings.Where(m => m.Isdeleted != true).ToList();
                var oldmappingids = group.Modifier.Select(s => s.MappingId).ToList();
                foreach (var map in oldmapping)
                {
                    if (oldmappingids.Contains(map.MappingId))
                    {
                        dbGroup.ModifierGroupMappings.Add(map);
                    }
                    else
                    {
                        map.Isdeleted = true;
                        dbGroup.ModifierGroupMappings.Add(map);
                    }
                }
                var newmapping = group.Modifier.Where(m => m.MappingId == 0).ToList();
                foreach (var map in newmapping)
                {
                    var mapping = new ModifierGroupMapping
                    {
                        ModifierId = map.Id
                    };
                    dbGroup.ModifierGroupMappings.Add(mapping);
                }
                result = _modifierGroupRepo.Update(group: dbGroup);
            }
        }
        return result;
    }

    public bool Delete(MenuDelete menuDelete)
    {

        switch (menuDelete.Name)
        {
            case "item":
                List<MenuItem> items = _itemRepository.ReadWithoutCategory().Where(s => menuDelete.Ids.Contains(s.IteamId)).ToList();
                foreach (var item in items)
                {
                    item.IsDeleted = true;
                };
                return _itemRepository.DeleteMany(items) ? true : false;

            case "modifier":
                // List<Modifier> modifiers = _modifierGroupMappingRepo.ReadModifierGroups().Where(s => menuDelete.Ids.Contains(s.ModifierId)).ToList();
                List<ModifierGroupMapping> mappings = new();
                foreach (var map in menuDelete.Ids)
                {
                    foreach(var element in _modifierGroupMappingRepo.ReadModifierGroups(map))
                    {
                        element.Isdeleted = true;
                        mappings.Add(element);
                    }
                }

                return _modifierGroupMappingRepo.UpdateMany(mappings) ? true : false;

            case "group":
                List<ModifierGroup> groups = _modifierGroupRepo.ReadAll().Where(s => menuDelete.Ids.Contains(s.ModGroupId)).ToList();
                foreach (var group in groups)
                {
                    group.IsDeleted = true;
                    foreach (var mapping in group.ModifierGroupMappings)
                    {
                        mapping.Isdeleted = true;
                    }
                    foreach(var mapping in group.ItemModifierGroups){
                        mapping.Isdeleted= true;
                    }
                }
                ;
                return _modifierGroupRepo.UpdateMany(groups) ? true : false;
                
            default:
                List<MenuCategory> categories = _categoryRepo.ReadAll().Where(s => menuDelete.Ids.Contains(s.CategoryId)).ToList();
                foreach (var category in categories)
                {
                    category.IsDeleted = true;
                    List<MenuItem> categoryItems = _itemRepository.ReadAll(categoryid: category.CategoryId).ToList();
                    foreach (var item in categoryItems){
                        item.IsDeleted = true;
                        category.MenuItems.Add(item);
                    }
                };
                return _categoryRepo.UpdateMany(categories) ? true : false;
        }

    }
    
    
    #region  check constrain

    public bool CheckConstrain(string name, string value)
    {
        return name switch
        {
            "category" => _categoryRepo.ReadAll().FirstOrDefault(o => o.MenuName.ToLower() == value.ToLower()) == null,
            "item" => _itemRepository.ReadWithoutCategory().FirstOrDefault(o => o.IteamName.ToLower() == value.ToLower()) == null,
            "group" => _modifierGroupRepo.ReadAll().FirstOrDefault(o => o.ModifierName.ToLower() == value.ToLower()) == null,
            _ => _modifiersRepo.ReadAll().FirstOrDefault(o => o.ModName.ToLower() == value.ToLower()) == null,
        };

    }

    #endregion

}
