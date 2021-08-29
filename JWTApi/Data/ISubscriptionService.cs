using System.Collections.Generic;
using System.Threading.Tasks;
using EBET.Dtos;
using EBET.Models;

namespace EBET.Data
{
    public interface ISubscriptionService
    {
        Task<Subscription> Subscribe(SubscriptionDto subscriptionDto);

        Task<IEnumerable<SubscribedCourseDto>> GetSubscribedCourse(int studentId);
    }
}
