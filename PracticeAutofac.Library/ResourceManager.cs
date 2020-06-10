using System.Collections.Generic;

namespace PracticeAutofac.Library
{
    public class ResourceManager
    {
        public IEnumerable<IResource> Resources{ get; set; }

        public ResourceManager(IEnumerable<IResource> resources)
        {
            Resources = resources;
        }
    }
}