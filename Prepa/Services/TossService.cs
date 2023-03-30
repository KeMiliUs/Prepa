using Microsoft.EntityFrameworkCore;
using Prepa.Database;
using Prepa.Models;
namespace Prepa.Services
{
    public interface ITossService
    {
        public Task<OnePupilTossDto[]> TassGroup(int groupId);
        public Task<RecipientPupilDto> GetReceptionist(int groupId, int pupilId);
    }
    public class TossService:ITossService
    {
        Random random = new Random();
        private DatabaseContext db;

        public TossService(DatabaseContext DB)
        {
            db = DB;
        }

        public async Task<OnePupilTossDto[]> TassGroup(int groupId)
        {
           
            var group = await db.Group.FirstOrDefaultAsync(x => x.id == groupId);
            if (group == null)
                throw new Exception("Not Found");
            var Pupils = db.Pupils.Where(x => x.GroupId == groupId).ToArray();
            if (Pupils.Length < 3 || Pupils.Length%2==1)
            {
                throw new Exception("Не подходящие условия для розыгрыша");
            }
            List<int> indexes= new List<int>();
            int ind = 0;
            foreach (var p in Pupils)
            {
                indexes.Add(ind);
                ind++;
            }
            ind = 0;
            while (indexes.Count > 0)
            {
                var partner = random.Next(indexes.Count);
                if (ind == indexes[partner])
                    continue;
                else
                {
                    Pupils[ind].recipient = Pupils[indexes[partner]];
                    indexes.Remove(indexes[partner]);
                    ind++;
                    await db.SaveChangesAsync();
                }
            }
            await db.SaveChangesAsync();
            OnePupilTossDto[] Sending= Pupils.Select(x=> new OnePupilTossDto { id = x.id, name = x.name, wish = x.wish,
            recipient= new RecipientPupilDto {name= x.recipient.name, id = x.recipient.id, wish = x.wish} }).ToArray();
            return Sending;
        }

        public async Task<RecipientPupilDto> GetReceptionist(int groupId, int pupilId)
        {
            var group = await db.Group.FirstOrDefaultAsync(x => x.id == groupId);
            if (group == null)
                throw new Exception("Not Found");
            var pupil = await db.Pupils.FirstOrDefaultAsync(x => x.id == pupilId);
            if (pupil == null)
                throw new Exception("Not Found");
            if (pupil.recipient == null)
                throw new Exception("Not Found");
            var arr = new RecipientPupilDto { wish = pupil.recipient.wish, id = pupil.recipient.id, name = pupil.recipient.name };

            return arr;

        }


    }
}
