using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Infrastructure;

namespace cv19ResSupportV3.V3.Factories.Commands
{
    public static class LookupFactory
    {
        public static LookupDomain ToDomain(this LookupEntity lookup)
        {
            return new LookupDomain() {Id = lookup.Id, Lookup = lookup.Lookup, LookupGroup = lookup.LookupGroup};
        }
        public static List<LookupDomain> ToDomain(this List<LookupEntity> lookup)
        {
            return lookup?.Select(l => l.ToDomain()).ToList();
        }
    }
}
