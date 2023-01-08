# PCDealerAPI
<b>Web API for PC Dealer React Project</b><br/>
<hr/>
The API has the following entities:
<b>
    <ul>
        <li>Brand</li>
        <li>CartItem</li>
        <li>Category</li>
        <li>ImageFile</li>
        <li>Model</li>
        <li>Order</li>
        <li>Product</li>
        <li>Review</li>
        <li>User</li>
    </ul>
</b>
It uses JWT authentication.<br />
Each entity has a controller with the following actions:
<ul>
    <li>
        <b>AccountController</b>
        <ul>
            <li>GetUserEmails - <i>/api/account/getUserEmails</i></li>
            <li>Register - <i>/api/account/register</i></li>
            <li>Login - <i>/api/account/login</i></li>
        </ul>
    </li>
    <li>
        <b>BrandController</b>
        <ul>
            <li>GetAllBrands - <i>/api/brand/getAllBrands</i></li>
            <li>GetBrand - <i>/api/brand/{brandId}</i></li>
            <li>AddBrand - <i>/api/brand/add</i></li>
            <li>UpdateBrand - <i>/api/brand/update/{brandId}</i></li>
            <li>DeleteBrand - <i>/api/brand/delete/{brandId}</i></li>
        </ul>
    </li>
    <li>
        <b>CategoryController</b>
        <ul>
            <li>AddCategory - <i>/api/category/add</i></li>
            <li>UpdateCategory - <i>/api/category/update/{categoryId}</i></li>
            <li>DeleteCategory - <i>/api/category/delete/{categoryId}</i></li>
        </ul>
    </li>
        <li>
        <b>ImageController</b>
        <ul>
            <li>GetImage - <i>/api/image/{imageId}</i></li>
            <li>GetAllProductImagesFullNames - <i>/api/image/all/{productId}</i></li>
        </ul>
    </li>
    <li>
        <b>ModelController</b>
        <ul>
            <li>GetAllBrandModels - <i>/api/model/brand/{brandId}/all</i></li>
            <li>GetModel - <i>/api/model/{modelId}</i></li>
            <li>AddModel - <i>/api/model/{brandId}/{categoryId?}/add</i></li>
            <li>UpdateModel - <i>/api/model/update/{modelId}/{categoryId?}</i></li>
            <li>DeleteModel - <i>/api/model/delete/{modelId}</i></li>
        </ul>
    </li>
        <li>
        <b>OrderController</b>
        <p>To access all order actions you <b>must be authenticated</b></p>
        <ul>
            <li>GetUserOrders - <i>/api/order/all</i></li>
            <li>GetOrder - <i>/api/order/{orderId}</i></li>
            <li>AddOrder - <i>/api/order/add</i></li>
            <li>DeleteОrder - <i>/api/order/delete/{orderId}</i></li>
        </ul>
    </li>
    <li>
        <b>ProductController</b>
        <ul>
            <li>GetAllProducts - <i>/api/product/all</i></li>
            <li>GetProduct - <i>/api/product/{productId}</i></li>
            <li>AddProduct - <i>/api/product/add</i></li>
            <li>UpdateProduct - <i>/api/product/update/{productId}/{modelId?}</i></li>
            <li>DeleteProduct - <i>/api/product/delete/{productId}</i></li>
        </ul>
    </li>
        <li>
        <b>ReviewController</b>
        <ul>
            <li>GetAllProductReviews - <i>/api/review/product/{productId}/all</i></li>
            <li>GetReview - <i>/api/review/{reviewId}</i></li>
            <p></p>
            <p>For the following actions you <b>must be authenticated</b>:</p>
            <li>AddReview - <i>/api/review/add</i></li>
            <li>UpdateReview - <i>/api/review/update/{reviewId}</i></li>
            <li>DeleteReview - <i>/api/review/delete/{reviewId}</i></li>
        </ul>
    </li>
</ul>
