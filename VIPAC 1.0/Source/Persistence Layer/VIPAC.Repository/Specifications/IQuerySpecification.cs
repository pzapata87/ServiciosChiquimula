using System.Linq;

namespace VIPAC.Repository.Specifications
{
    public interface IQuerySpecification<T>
    {
        IQueryable<T> SatisfyingElementsFrom(IQueryable<T> candidates);
    }
}