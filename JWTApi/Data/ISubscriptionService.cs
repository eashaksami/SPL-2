using System.Collections.Generic;
using System.Threading.Tasks;
using JWTApi.Dtos;
using JWTApi.Models;

namespace JWTApi.Data
{
    public interface ISubscriptionService
    {
        Task<Subscription> Subscribe(SubscriptionDto subscriptionDto);

        Task<IEnumerable<SubscribedCourseDto>> GetSubscribedCourse(int studentId);
    }
}