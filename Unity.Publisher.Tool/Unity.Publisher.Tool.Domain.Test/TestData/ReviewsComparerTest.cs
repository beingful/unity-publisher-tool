using Unity.Publisher.Tool.Domain.Business.Publisher.Models;

namespace Unity.Publisher.Tool.Domain.Test.Comparers;

public partial class ReviewsComparerTest
{
    public static IEnumerable<object[]> EqualReviews()
    {
        Reviews reviews = new(collection:
            [
                new Review(
                    id: 1,
                    product: "Product",
                    subject: "Cool!",
                    body: "Cool Product!",
                    rating: 5,
                    created: DateTime.Now),
                new Review(
                    id: 2,
                    product: "Product",
                    subject: "Cool!",
                    body: "Cool Product!",
                    rating: 5,
                    created: DateTime.Now)
            ]);

        Reviews sameReviews = new(collection:
            [
                new Review(
                    id: 1,
                    product: "Product",
                    subject: "Cool!",
                    body: "Cool Product!",
                    rating: 5,
                    created: DateTime.Now),
                new Review(
                    id: 2,
                    product: "Product",
                    subject: "Cool!",
                    body: "Cool Product!",
                    rating: 5,
                    created: DateTime.Now)
            ]);

        Reviews sameReviewsUpdated = new(collection:
            [
                new Review(
                    id: 1,
                    product: "Product",
                    subject: "Nice!",
                    body: "Nice Product!",
                    rating: 5,
                    created: DateTime.Now),
                new Review(
                    id: 2,
                    product: "Product",
                    subject: "Nice!",
                    body: "Nice Product!",
                    rating: 5,
                    created: DateTime.Now)
            ]);

        return [
            [reviews, sameReviews],
            [reviews, sameReviewsUpdated],
            [Reviews.Empty(), Reviews.Empty()]
        ];
    }

    public static IEnumerable<object[]> DifferentReviews()
    {
        Reviews reviews = new(collection:
            [
                new Review(
                    id: 1,
                    product: "Product",
                    subject: "Cool!",
                    body: "Cool Product!",
                    rating: 5,
                    created: DateTime.Now),
                new Review(
                    id: 2,
                    product: "Product",
                    subject: "Cool!",
                    body: "Cool Product!",
                    rating: 5,
                    created: DateTime.Now)
            ]);

        Reviews oneReviewAdded = new(collection:
            [
                new Review(
                    id: 1,
                    product: "Product",
                    subject: "Cool!",
                    body: "Cool Product!",
                    rating: 5,
                    created: DateTime.Now),
                new Review(
                    id: 2,
                    product: "Product",
                    subject: "Cool!",
                    body: "Cool Product!",
                    rating: 5,
                    created: DateTime.Now),
                new Review(
                    id: 3,
                    product: "Product",
                    subject: "Cool!",
                    body: "Cool Product!",
                    rating: 5,
                    created: DateTime.Now)
            ]);

        Reviews oneReviewRemoved = new(collection:
            [
                new Review(
                    id: 1,
                    product: "Product",
                    subject: "Nice!",
                    body: "Nice Product!",
                    rating: 5,
                    created: DateTime.Now)
            ]);

        Reviews differentReviews = new(collection:
            [
                new Review(
                    id: 4,
                    product: "Product",
                    subject: "Nice!",
                    body: "Nice Product!",
                    rating: 5,
                    created: DateTime.Now),
                new Review(
                    id: 5,
                    product: "Product",
                    subject: "Nice!",
                    body: "Nice Product!",
                    rating: 5,
                    created: DateTime.Now)
            ]);

        return [
            [reviews, oneReviewAdded],
            [reviews, oneReviewRemoved],
            [reviews, differentReviews],
            [reviews, Reviews.Empty()],
            [Reviews.Empty(), reviews]
        ];
    }

    public static IEnumerable<object[]> ReviewsWithEmptyDifference()
    {
        DateTime now = DateTime.Now;

        Review firstReview = new(
            id: 1,
            product: "Product",
            subject: "Cool!",
            body: "Cool Product!",
            rating: 5,
            created: now);

        Review secondReview = new(
            id: 1,
            product: "Product",
            subject: "Cool!",
            body: "Cool Product!",
            rating: 5,
            created: now);

        Reviews reviews = new(collection: [firstReview, secondReview]);

        Reviews reviewRemoved = new(collection: [firstReview]);

        return [
                ..EqualReviews(),
                [Reviews.Empty(), reviews],
                [reviewRemoved, reviews]
            ];
    }

    public static IEnumerable<object[]> ReviewsWithNotEmptyDifference()
    {
        DateTime now = DateTime.Now;

        Review firstReview = new(
            id: 1,
            product: "Product",
            subject: "Cool!",
            body: "Cool Product!",
            rating: 5,
            created: now);

        Review firstReviewUpdated = new(
            id: 1,
            product: "Product",
            subject: "Nice!",
            body: "Nice Product!",
            rating: 5,
            created: now);

        Review secondReview = new(
            id: 1,
            product: "Product",
            subject: "Cool!",
            body: "Cool Product!",
            rating: 5,
            created: now);

        Review thirdReview = new(
            id: 3,
            product: "Product",
            subject: "Cool!",
            body: "Cool Product!",
            rating: 3,
            created: now);

        Reviews reviews = new(collection: [firstReview, secondReview]);

        Reviews newReviewAdded = new(collection: [firstReview, secondReview, thirdReview]);

        Reviews newReviewAddedDifference = new(collection: [thirdReview]);

        Reviews allReviewsReplaced = new(collection: [thirdReview]);

        Reviews allReviewsReplacedDifference = new(collection: [thirdReview]);

        return [
            [reviews, Reviews.Empty(), reviews],
            [newReviewAdded, reviews, newReviewAddedDifference],
            [allReviewsReplaced, reviews, allReviewsReplacedDifference]
        ];
    }
}
