using System.Linq.Expressions;

namespace GenericMinimalApi.Infrastructures.Http
{
    public class CRUDHttpHandlerConfiguration<TEntity>
    {
        public string? Entity { get; set; }
        public bool EnableList { get; set; }= true;
        public bool EnableGetById { get; set; } = true;
        public bool EnablePost { get; set; }= true;
        public bool EnablePut { get; set; } = true;
        public bool EnableDelete { get; set; } = true;
        public bool RequiredAutorize { get; set; } = false;
        public bool RequiredAutorizeList { get; set; } = true;
        public bool RequiredAutorizeGetById { get; set; } = true;
        public bool RequiredAutorizePost { get; set; } = true;
        public bool RequiredAutorizePut { get; set; } = true;
        public bool RequiredAutorizeDelete { get; set; } = true;
        public Func<string, Expression<Func<TEntity, bool>>> TextFilterFunc { get; set; }
    }
}