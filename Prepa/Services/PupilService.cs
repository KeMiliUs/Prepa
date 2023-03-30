using Prepa.Models;
using Prepa.Database;
using Microsoft.EntityFrameworkCore;

namespace Prepa.Services
{   public interface IPupilService
    {
        public Task<int> CreatePupil(AddPupilDto model,int group_id);
        public Task DeletePupil(int pupil_id, int group_id);
    }
    public class PupilService:IPupilService
    {
        private DatabaseContext db;

        public PupilService(DatabaseContext DB)
        {
            db = DB;
        }
        public async Task<int> CreatePupil(AddPupilDto model, int group_id)
        {
            var group = await db.Group.FirstOrDefaultAsync(x => x.id == group_id);
            if (group == null)
                throw new Exception("Not Found");
            var pupil = new Pupil() { GroupId = group_id, name = model.name, wish = model.wish is null ? "" : model.wish };
            await db.Pupils.AddAsync(pupil);
            await db.SaveChangesAsync();
            return pupil.id;
        }

        public async Task DeletePupil(int pupil_id, int group_id)
        {
            var group = await db.Group.FirstOrDefaultAsync(x => x.id == group_id);
            if (group == null)
                throw new Exception("Not Found");
            var pupil = await db.Pupils.FirstOrDefaultAsync(x => x.id == pupil_id);
            if (pupil == null)
                throw new Exception("Not Found");
            if (pupil.recipient != null)
            {
                if(pupil.recipient.recipient != null)
                {
                    if (pupil.recipient.recipient.id == pupil_id) // если люди дарят друг и друга и чел удаляется, то
                        pupil.recipient.recipient = null;
                }
            }
            db.Pupils.Remove(pupil);
            await db.SaveChangesAsync();
        }
    }
}
