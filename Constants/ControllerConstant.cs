namespace Constants
{
    public static class ControllerConstant
    {
        public const string CORS_POLICY = "MyCorsPolicy";

        public const string MULTIPART_FORM_DATA_REQUEST_TYPE = "multipart/form-data";

        public const string CONTROLLER_BASE_ROUTE = "api/[controller]";

        public const string ALL_ROUTE = "all";

        public const string ADD_ROUTE = "add";

        public const string UPDATE_ROUTE = "update/";

        public const string DELETE_ROUTE = "delete/";

        public const string GET_USER_EMAILS_ROUTE = "getUserEmails";

        public const string REGISTER_ROUTE = "register";

        public const string LOGIN_ROUTE = "login";

        public const string BRAND_ID_PARAMETER = "{brandId}";

        public const string CATEGORY_ID_PARAMETER = "{categoryId}";

        public const string CATEGORY_ID_OPTIONAL_PARAMETER = "{categoryId?}";

        public const string MODEL_ID_PARAMETER = "{modelId}";

        public const string MODEL_ID_OPTIONAL_PARAMETER = "{modelId?}";

        public const string ORDER_ID_PARAMETER = "{orderId}";

        public const string PRODUCT_ID_PARAMETER = "{productId}";

        public const string IMAGE_ID_PARAMETER = "{imageId}";

        public const string REVIEW_ID_PARAMETER = "{reviewId}";

        public const string GET_PRODUCTS_BY_CATEGORY_ROUTE = $"category/{CATEGORY_ID_PARAMETER}/{ALL_ROUTE}";

        public const string ADD_PRODUCT_ROUTE = $"model/{MODEL_ID_PARAMETER}/{ADD_ROUTE}";

        public const string GET_ALL_REVIEWS_FOR_PRODUCT_ROUTE = $"product/{PRODUCT_ID_PARAMETER}/{ALL_ROUTE}";

        public const string ADD_PRODUCT_REVIEW = $"product/{PRODUCT_ID_PARAMETER}/{ADD_ROUTE}";
    }
}