using Undersoft.SDK.Invoking;
using Undersoft.SDK.Rubrics;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Application.GUI.View.Generic.Validator;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Operation;
using Undersoft.SDK.Updating;

namespace Undersoft.SDK.Service.Application.GUI.View.Attributes
{
    public static class ViewAttributes
    {
        public static ISeries<IInvoker> Registry;
        public static ViewAttributeResolvers ViewResolveAttributes;

        static ViewAttributes()
        {
            Registry = new Registry<IInvoker>();
            ViewResolveAttributes = new ViewAttributeResolvers();

            Registry.Add(
                typeof(ViewClassAttribute),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveClassAttributes
                )
            );
            Registry.Add(
                typeof(ViewStyleAttribute),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveStyleAttributes
                )
            );
            Registry.Add(
                typeof(GridAttribute),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveGridAttributes
                )
            );
            Registry.Add(
                typeof(StackAttribute),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveStackAttributes
                )
            );
            Registry.Add(
                typeof(MenuItemAttribute),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveMenuItemAttributes
                )
            );
            Registry.Add(
                typeof(MenuGroupAttribute),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveMenuGroupAttributes
                )
            );
            Registry.Add(
                typeof(ValidatorAttribute),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveValidatorAttributes
                )
            );
            Registry.Add(
                typeof(ViewImageAttribute),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveImageAttributes
                )
            );
            Registry.Add(
                typeof(ViewSizeAttribute),
                new Invoker<ViewAttributeResolvers>(
                    ViewResolveAttributes,
                    m => m.ResolveSizeAttributes
                )
            );
        }

        public static IViewData Resolve(IViewData mr)
        {
            var customAttributes = mr.Model.GetType().GetCustomAttributes(false);

            if (customAttributes.Any())
            {
                var duplicateCheck = new HashSet<string>();

                customAttributes.ForEach(a =>
                {
                    var type = a.GetType();
                    if (
                        ViewAttributes.Registry.TryGet(type, out IInvoker invoker)
                        && duplicateCheck.Add(invoker.MethodName)
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
                var duplicateCheck = new HashSet<string>();

                customAttributes.ForEach(a =>
                {
                    var type = a.GetType();
                    if (
                        ViewAttributes.Registry.TryGet(type, out IInvoker invoker)
                        && duplicateCheck.Add(invoker.MethodName)
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

        public void ResolveSizeAttributes(IViewData data)
        {
            var modelType = data.ModelType;

            object? o = modelType
                .GetCustomAttributes(typeof(ViewSizeAttribute), false)
                .FirstOrDefault();
            if ((o != null))
            {
                ViewSizeAttribute fta = (ViewSizeAttribute)o;

                data.Width = fta.Width;
                data.Height = fta.Height;
                data.Z = fta.Z;
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
                mr.Width = fta.Width;
                mr.Height = fta.Height;
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
            var modelType = data.ModelType;

            object? o = modelType
                .GetCustomAttributes(typeof(ValidatorAttribute), false)
                .FirstOrDefault();
            if ((o != null))
            {
                ValidatorAttribute fta = (ValidatorAttribute)o;
                data.ValidatorType = fta.ValidatorType;
                data.ValidatorTypeName = fta.ValidatorTypeName;
                if (data.ValidatorType != null && data.Validator == null)
                {
                    data.Validator = typeof(GenericValidator<,>)
                        .MakeGenericType(data.ValidatorType, modelType)
                        .New<IViewValidator>();
                }
            }
        }
    }
}
