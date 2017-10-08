//using System;
//using System.Threading.Tasks;
//using Microsoft.Owin;
//using Owin;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;

//[assembly: OwinStartup(typeof(sensoresapp.GoogleMaps.POC_gmapsEntities))]

//namespace sensoresapp.GoogleMaps

//{
//    public partial class POC_gmapsEntities : DbContext
//    {
//        protected POC_gmapsEntities(DbCompiledModel model) : base(model)
//        {
//        }
//    }
//    public class POC_gmapsEntities(): base("name = POC_gmapsEntities")
//    {
//        }
//     protected override void onModelCreating(dbModelBuilder)
//        //public void Configuration(IAppBuilder app)
//        {
//           throw new UnintentionalCodeFirstException();
//            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
//        }
//public DbSet<Ubicacion> Ubicacion { get; set; }
//    }
//}
