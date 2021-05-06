using KILROY.Base;

namespace KILROY.Project.Model
{
    public abstract class ProjectProxy : BaseProxy
    {
        public ProjectProxy(string name, object data = null) : base(name, data) { }
    }
}