using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notat.Models
{
    public class Picture
    {
        public string DescPicture { get; set; }
        public string SourcePicture { get; set; }

        public Picture()
        {

        }

        public Picture(string descPicture, string sourcePicture)
        {
            this.DescPicture = descPicture;
            this.SourcePicture = sourcePicture;
        }
    }
}
