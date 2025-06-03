using Microsoft.EntityFrameworkCore;
using pizzashop.data.Models;
using pizzashop.repository.Interfaces;

namespace pizzashop.repository.Implementations;

public class ReviewRepository: IReviewRepository
{

    private readonly PizzashopContext _db;

    public ReviewRepository(PizzashopContext db)
    {
        _db = db;
    }

    public bool AddUpdateMany(List<Review> reviews, int status)
    {
        try
        {
            if (status == 0)
            {
                _db.Reviews.AddRange(reviews);
            }
            else
            {
                _db.Reviews.UpdateRange(reviews);
            }
            _db.SaveChanges();
            return true;
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return false;
    }


    public bool AddUpdate(Review reviews)
    {
        try
        {
            if (reviews.Id == 0)
            {
                _db.Reviews.Add(reviews);
            }
            else
            {
                _db.Reviews.Update(reviews);
            }
            _db.SaveChanges();
            return true;
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return false;
    }

    public Review Read(int reviewsid)
    {
        try
        {
            Review reviews = _db.Reviews.Find(reviewsid);
            return reviews;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return new Review();
    }

    public IEnumerable<Review> ReadAll(int customerid)
    {
        try
        {
            IEnumerable<Review> reviews = _db.Reviews.Where(x => x.CustomerId == customerid).ToList();
            return reviews;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return new List<Review>();
    }

}
