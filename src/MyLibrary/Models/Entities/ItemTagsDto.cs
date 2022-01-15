using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Models.Entities
{
    public class ItemTagsDto : Entity
    {
        public IEnumerable<string> TagsToAdd { get; private set; }
        public IEnumerable<string> TagsToRemove { get; private set; }

        public ItemTagsDto(int itemId,
            IEnumerable<string> originalTags, IEnumerable<string> selectedTags)
        {
            this.Id = itemId;

            this.TagsToAdd = from st in selectedTags
                             where !originalTags.Contains(st)
                             select st;

            this.TagsToRemove = from ot in originalTags
                                where !selectedTags.Contains(ot)
                                select ot;
        }
    }//class
}
