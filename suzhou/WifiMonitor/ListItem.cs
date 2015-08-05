using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WifiMonitor
{
    class ListItem
    {
        private string id = string.Empty;
        private string name = string.Empty;

        public ListItem(string sid, string sname)
        {
            id = sid;
            name = sname;
        }

        public override string ToString()
        {
            return this.name;
        }

        /*combobox的 Item.ADD(一个任意类型的变量），而显示的时候调用的是这个变量的ToString()方法，如果这个类没有重载ToString()，那么显示的结果就是命名空间   +   类名*/

        public string ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        } 
    }
}
