using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceGeneration.ResourceVariationen
    {
    interface IResources
        {
        string ResourceName { get; }
        int QuantityOfTheResource { get; set; }
        void ResourceQuantityCheck();
            }
    }
