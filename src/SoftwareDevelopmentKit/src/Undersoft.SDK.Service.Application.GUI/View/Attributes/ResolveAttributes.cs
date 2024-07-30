using Undersoft.SDK.Invoking;
using Undersoft.SDK.Rubrics;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Generic.Validator;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Data.Query;
using Undersoft.SDK.Service.Operation;
using Undersoft.SDK.Uniques;
using Undersoft.SDK.Updating;
using Undersoft.SDK.Utilities;

namespace Undersoft.SDK.Service.Application.GUI.View.Attributes
{
    public static class ViewAttributes
    {
        public static ITypedSeries<IInvoker> Registry;
        public static ViewAttributeResolvers ViewResolveAttributes;

        static ViewAttributes()
        {
            Registry = new TypedRegistry<IInvoker>();
            ViewResolveAttributes = new ViewAttributeResolvers();

            Registry.Add(
                typeof(ViewClassAttribute),
                nameof(ViewRubric).UniqueKey32(),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveClassAttributes
                )
            );
            Registry.Add(
                typeof(ViewStyleAttribute),
                nameof(ViewRubric).UniqueKey32(),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveStyleAttributes
                )
            );
            Registry.Add(
                typeof(GridAttribute),
                nameof(ViewRubric).UniqueKey32(),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveGridAttributes
                )
            );
            Registry.Add(
                typeof(StackAttribute),
                nameof(ViewRubric).UniqueKey32(),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveStackAttributes
                )
            );
            Registry.Add(
                typeof(MenuItemAttribute),
                nameof(ViewRubric).UniqueKey32(),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveMenuItemAttributes
                )
            );
            Registry.Add(
                typeof(MenuGroupAttribute),
                nameof(ViewRubric).UniqueKey32(),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveMenuGroupAttributes
                )
            );
            Registry.Add(
                typeof(ValidatorAttribute),
                nameof(IViewData).UniqueKey32(),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveValidatorAttributes
                )
            );
            Registry.Add(
                typeof(ViewSizeAttribute),
                nameof(IViewData).UniqueKey32(),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveSizeClassAttributes
                )
            );
            Registry.Add(
                typeof(ViewImageAttribute),
                nameof(ViewRubric).UniqueKey32(),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveImageAttributes
                )
            );
            Registry.Add(
                typeof(ViewSizeAttribute),
                nameof(ViewRubric).UniqueKey32(),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveSizeRubricAttributes
                )
            );
            Registry.Add(
                typeof(OpenQueryAttribute),
                nameof(ViewRubric).UniqueKey32(),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveQueryRubricAttributes
                )
            );
            Registry.Add(
                typeof(OpenSearchAttribute),
                nameof(IViewData).UniqueKey32(),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveSearchAttributes
                )
            );
            Registry.Add(
                typeof(GridAttribute),
                nameof(IViewData).UniqueKey32(),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveGridClassAttributes
                )
            );
            Registry.Add(
                typeof(StackAttribute),
                nameof(IViewData).UniqueKey32(),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveStackClassAttributes
                )
            );
        }

        public static IViewData Resolve(IViewData mr)
        {
            var customAttributes = mr.Model.GetType().GetCustomAttributes(false);

            if (customAttributes.Any())
            {
                customAttributes.ForEach(a =>
                {
                    var type = a.GetType();
                    if (
                        ViewAttributes.Registry.TryGet(
                            type,
                            nameof(IViewData).UniqueKey32(),
                            out IInvoker invoker
                        )
                    )
                    {
                        invoker.Invoke(mr);
                    }
                });
            }
            return mr;
        }

        public static ViewRubric Resolve(ViewRubric mr)
        {
            var mi = ((IMemberRubric)mr.RubricInfo).MemberInfo;

            var customAttributes = mi.GetCustomAttributes(false);

            if (customAttributes.Any())
            {
                customAttributes.ForEach(a =>
                {
                    var type = a.GetType();
                    if (
                        ViewAttributes.Registry.TryGet(
                            type,
                            nameof(ViewRubric).UniqueKey32(),
                            out IInvoker invoker
                        )
                    )
                    {
                        invoker.Invoke(mr);
                    }
                });
            }
            return mr;
        }
    }

    public class ViewAttributeResolvers
    {
        public void ResolveClassAttributes(ViewRubric mr)
        {
            var mi = ((IMemberRubric)mr.RubricInfo).MemberInfo;

            object? o = mi.GetCustomAttributes(typeof(ViewClassAttribute), false).FirstOrDefault();
            if ((o != null))
            {
                ViewClassAttribute fta = (ViewClassAttribute)o;

                mr.Class = fta.Class;
            }
        }

        public void ResolveStyleAttributes(ViewRubric mr)
        {
            var mi = ((IMemberRubric)mr.RubricInfo).MemberInfo;

            object? o = mi.GetCustomAttributes(typeof(ViewStyleAttribute), false).FirstOrDefault();
            if ((o != null))
            {
                ViewStyleAttribute fta = (ViewStyleAttribute)o;

                mr.Style = fta.Style;
            }
        }

        public void ResolveSizeClassAttributes(IViewData data)
        {
            if (data.Z != null)
                return;

            var modelType = data.ModelType;

            object? o = modelType
                .GetCustomAttributes(typeof(ViewSizeAttribute), false)
                .FirstOrDefault();
            if ((o != null))
            {
                ViewSizeAttribute fta = (ViewSizeAttribute)o;

                data.Width = fta.Width;
                data.Height = fta.Height;
                data.Z = fta.Z == null ? "" : fta.Z;
            }
        }

        public void ResolveSizeRubricAttributes(ViewRubric mr)
        {
            if (mr.Z != null)
                return;

            var mi = ((IMemberRubric)mr.RubricInfo).MemberInfo;

            object? o = mi.GetCustomAttributes(typeof(ViewSizeAttribute), false).FirstOrDefault();
            if ((o != null))
            {
                ViewSizeAttribute fta = (ViewSizeAttribute)o;

                mr.Width = fta.Width;
                mr.Height = fta.Height;
                mr.Z = fta.Z == null ? "" : fta.Z;
            }
        }

        public void ResolveQueryRubricAttributes(ViewRubric mr)
        {
            var mi = ((IMemberRubric)mr.RubricInfo).MemberInfo;

            object? o = mi.GetCustomAttributes(typeof(OpenQueryAttribute), false).FirstOrDefault();
            if ((o != null))
            {
                OpenQueryAttribute fta = (OpenQueryAttribute)o;

                mr.FilterMembers = fta.FilterMembers;
                mr.SortMembers = fta.SortMembers;
                mr.FilteredType = fta.FilteredType ?? mr.RubricType;
            }
        }

        public void ResolveSearchAttributes(IViewData data)
        {
            var modelType = data.ModelType;

            object? o = modelType
                .GetCustomAttributes(typeof(OpenSearchAttribute), false)
                .FirstOrDefault();
            if ((o != null))
            {
                OpenSearchAttribute fta = (OpenSearchAttribute)o;

                data.SearchMembers = fta.SearchMembers;
            }
        }

        public void ResolveImageAttributes(ViewRubric mr)
        {
            var mi = ((IMemberRubric)mr.RubricInfo).MemberInfo;

            object? o = mi.GetCustomAttributes(typeof(ViewImageAttribute), false).FirstOrDefault();
            if ((o != null))
            {
                ViewImageAttribute fta = (ViewImageAttribute)o;

                mr.ImageMode = fta.Mode;
                mr.ImageWidth = fta.Width;
                mr.ImageHeight = fta.Height;
                mr.Z = fta.Z;
            }
        }

        public void ResolveGridAttributes(ViewRubric mr)
        {
            var mi = ((IMemberRubric)mr.RubricInfo).MemberInfo;

            object? o = mi.GetCustomAttributes(typeof(GridAttribute), false).FirstOrDefault();
            if ((o != null))
            {
                GridAttribute fta = (GridAttribute)o;

                mr.Grid = fta.PutTo<ViewGrid>();
            }
        }

        public void ResolveStackAttributes(ViewRubric mr)
        {
            var mi = ((IMemberRubric)mr.RubricInfo).MemberInfo;

            object? o = mi.GetCustomAttributes(typeof(StackAttribute), false).FirstOrDefault();
            if ((o != null))
            {
                StackAttribute fta = (StackAttribute)o;

                mr.Stack = fta.PutTo<ViewStack>();
            }
        }

        public void ResolveMenuItemAttributes(ViewRubric mr)
        {
            var mi = ((IMemberRubric)mr.RubricInfo).MemberInfo;

            object? o = mi.GetCustomAttributes(typeof(MenuItemAttribute), false).FirstOrDefault();
            if ((o != null))
            {
                mr.IsMenuItem = true;
            }
        }

        public void ResolveMenuGroupAttributes(ViewRubric mr)
        {
            var mi = ((IMemberRubric)mr.RubricInfo).MemberInfo;

            object? o = mi.GetCustomAttributes(typeof(MenuGroupAttribute), false).FirstOrDefault();
            if ((o != null))
            {
                mr.IsMenuItem = true;
                mr.IsMenuGroup = true;
            }
        }

        public void ResolveValidatorAttributes(IViewData data)
        {
            if (data.Validator != null)
                return;

            var modelType = data.ModelType;

            object? o = modelType
                .GetCustomAttributes(typeof(ValidatorAttribute), false)
                .FirstOrDefault();
            if ((o != null))
            {
                ValidatorAttribute fta = (ValidatorAttribute)o;
                data.ValidatorType = fta.ValidatorType;
                data.ValidatorTypeName = fta.ValidatorTypeName;

                if (data.ValidatorType == null)
                    return;

                data.Validator = typeof(GenericValidator<,>)
                    .MakeGenericType(data.ValidatorType, modelType)
                    .New<IViewValidator>();
            }
        }

        public void ResolveGridClassAttributes(IViewData data)
        {
            if (data.Validator != null)
                return;

            var modelType = data.ModelType;

            object? o = modelType
                .GetCustomAttributes(typeof(GridAttribute), false)
                .FirstOrDefault();
            if ((o != null))
            {
                GridAttribute fta = (GridAttribute)o;

                data.Grid = fta.PutTo<ViewGrid>();
            }
        }

        public void ResolveStackClassAttributes(IViewData data)
        {
            if (data.Validator != null)
                return;

            var modelType = data.ModelType;

            object? o = modelType
                .GetCustomAttributes(typeof(StackAttribute), false)
                .FirstOrDefault();
            if ((o != null))
            {
                StackAttribute fta = (StackAttribute)o;

                data.Stack = fta.PutTo<ViewStack>();
            }
        }
    }
}
