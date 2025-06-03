using pizzashop.data.Models;

namespace pizzashop.repository.Interfaces;

public interface IReviewRepository
{
    public bool AddUpdateMany(List<Review> reviews, int status);
    public bool AddUpdate(Review reviews);
    public Review Read(int reviewsid);
    public IEnumerable<Review> ReadAll(int customerid);
    
}
