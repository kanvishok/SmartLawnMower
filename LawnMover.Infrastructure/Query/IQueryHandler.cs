using System.Threading.Tasks;

namespace LawnMower.Infrastructure.Query
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery:IQuery<TResult>
    {
        Task<TResult> RetriveAsync(TQuery query);
    }
}