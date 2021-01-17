using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFramework.Core.CrossCuttingConcerns.Validation.FluentValidation;
using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.ValidationRules.FluentValidation;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using DevFramework.Core.Aspects.PostSharp.ValidationAspects;
using DevFramework.Core.Aspects.PostSharp.TransactionAspects;
using DevFramework.Core.DataAccess;
using System.Transactions;
using DevFramework.Core.Aspects.PostSharp.CacheAspects;
using DevFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using DevFramework.Core.Aspects.PostSharp.LogAspects;
using DevFramework.Core.Aspects.PostSharp.PerformanceAspects;
using System.Threading;
using DevFramework.Core.Aspects.PostSharp.AuthourizationAspects;

namespace DevFramework.Northwind.Business.Concrete.Managers
{

    public class ProductManager : IProductService
    {
       
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        [FluentValidationAspect(typeof(ProductValidatior))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Product Add(Product product)
        {
            ValidatorTool.FluentValidate(new ProductValidatior(), product);
            return _productDal.Add(product);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        [PerformanceCounterAspect(2)]
        [SecuredOperation(Roles="Admin, Editor, Student")]
        public List<Product> GetAll()
        {
            Thread.Sleep(3000);
            return _productDal.GetList();
        }

        public Product GetById(int id)
        {
            return _productDal.Get(p => p.ProductId == id);
        }
        [TransactionScopeAspect]
        [FluentValidationAspect(typeof(ProductValidatior))]
        public void TransactionalOperation(Product product1, Product product2)
        {
            _productDal.Add(product1);
            //Business code
            _productDal.Update(product2);
        }
        
        [FluentValidationAspect(typeof(ProductValidatior))]
        public Product Update(Product product)
        {
            ValidatorTool.FluentValidate(new ProductValidatior(), product);
            return _productDal.Update(product);
        }
    }
}
