using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mjolnir.IDE.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class NewContentAttribute : Attribute
    {
        public NewContentAttribute(string display, int priority, string description = "", string imageSource = "")
        {
            this.Display = display;
            if (!string.IsNullOrEmpty(imageSource))
            {
                this.ImageSource = new BitmapImage(new Uri(imageSource));
            }
            this.Priority = priority;
            this.Description = description;
        }

        public string Display { get; private set; }

        public ImageSource ImageSource { get; private set; }

        public int Priority { get; private set; }

        public string Description { get; private set; }
    }
}