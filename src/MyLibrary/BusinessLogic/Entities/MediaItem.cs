using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Models.Entities
{
    public class MediaItem : Item
    {
        private ItemType _type;
        public override ItemType Type
        {
            get => this._type;
            set
            {
                if (value == ItemType.Book)
                    throw new InvalidOperationException("Cannot set MediaItem.Type to ItemType.Book");

                this._type = value;
            } 
        }

        public long Number { get; set; }
        public int? RunningTime { get; set; }
        public int ReleaseYear { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(base.ToString());

            builder.AppendLine();
            builder.AppendLine();

            ToStringAppendField(builder, "Number: ", this.Number.ToString());

            builder.AppendLine("Running Time: ");
            if (!(RunningTime is null))
            {
                builder.AppendLine(this.RunningTime.ToString());
                builder.AppendLine();
            }
            else
            {
                builder.AppendLine();
                builder.AppendLine();
            }
            builder.AppendLine("Release Year: ");
            builder.Append(this.ReleaseYear.ToString());

            return builder.ToString();
        }//ToString
    }//class
}
