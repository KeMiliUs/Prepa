using Prepa.Models.Group;
using Prepa.Database;
using Microsoft.EntityFrameworkCore;

namespace Prepa.Services
{
    public interface IGroupControlService
    {
        public Task<int> CreateGroup(CreateGroupDto model);
        public SendAllGroupDto[] GetAllGroups();

        public Task ChangeGroup(ChangeGroupDto model, int id);
        public Task DeleteGroup(int id);

    }
    public class GroupControlService:IGroupControlService
    {
        private DatabaseContext db;

        public GroupControlService(DatabaseContext DB)
        {
            db = DB;
        }
        public async Task<int> CreateGroup(CreateGroupDto model)
        {
            Group newGroup = new Group() { description = model.description is null ? "" : model.description, name = model.name };
            await db.Group.AddAsync(newGroup);
            await db.SaveChangesAsync();
            return newGroup.id;
        }
        public SendAllGroupDto[] GetAllGroups()
        {
            var group = db.Group.Select(x => new SendAllGroupDto { description = x.description, id = x.id, name = x.name }).ToArray();
            return group;
        }
        public async Task ChangeGroup(ChangeGroupDto model, int id)
        {
            var group = await db.Group.FirstOrDefaultAsync(x => x.id == id);
            if (group == null)
                throw new Exception("Not Found");
            group.description = model.description is null ? "" : model.description;
            group.name = model.name is null ? group.name : model.name;
            await db.SaveChangesAsync();
        }
        public async Task DeleteGroup(int id)
        {
            var group = await db.Group.FirstOrDefaultAsync(x => x.id == id);
            if (group == null)
                throw new Exception("Not Found");
            else
            {
                db.Group.Remove(group);
                await db.SaveChangesAsync();
            }
        }

    }
}
