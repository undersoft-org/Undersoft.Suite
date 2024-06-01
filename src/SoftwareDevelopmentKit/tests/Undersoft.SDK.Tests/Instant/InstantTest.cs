namespace Undersoft.SDK.Tests.Instant
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Reflection;
    using Undersoft.SDK.Instant;
    using Undersoft.SDK.Uniques;

    [TestClass]
    public class InstantTest
    {
        public InstantTest() { }

        [TestMethod]
        public void Instant_Memberinfo_FieldsAndPropertiesModel_Integration_Test()
        {
            InstantCreator derivedType = new InstantCreator<Agreement>(InstantType.Derived);
            IInstant InstantA = Instant_Creation_Compilation_Get_Set_Property_Operations_On_New_RuntimeType_Object_Instance_Test(derivedType, new Agreement());

            InstantCreator referenceType = new InstantCreator<Agreement>();
            IInstant InstantB = Instant_Creation_Compilation_Get_Set_Property_Operations_On_New_RuntimeType_Object_Instance_Test(referenceType, new Agreement());

            InstantCreator valueType = new InstantCreator<Agreement>(InstantType.ValueType);
            IInstant InstantC = Instant_Creation_Compilation_Get_Set_Property_Operations_On_New_RuntimeType_Object_Instance_Test(valueType, new Agreement());
        }

        [TestMethod]
        public void Instant_Memberinfo_FieldsOnlyModel_Integration_Test()
        {
            InstantCreator derivedType = new InstantCreator<FieldsOnlyModel>(InstantType.Derived);
            IInstant InstantA = Instant_Creation_Compilation_Get_Set_Property_Operations_On_New_RuntimeType_Object_Instance_Test(derivedType, new FieldsOnlyModel());

            InstantCreator referenceType = new InstantCreator(typeof(FieldsOnlyModel), InstantType.Reference);
            IInstant InstantB = Instant_Creation_Compilation_Get_Set_Property_Operations_On_New_RuntimeType_Object_Instance_Test(referenceType, new FieldsOnlyModel());
        }

        [TestMethod]
        public void Instant_Memberinfo_PropertiesOnlyModel_Integration_Test()
        {
            InstantCreator derivedType = new InstantCreator(typeof(PropertiesOnlyModel), InstantType.Derived);
            IInstant InstantA = Instant_Creation_Compilation_Get_Set_Property_Operations_On_New_RuntimeType_Object_Instance_Test(
                derivedType,
                new PropertiesOnlyModel()
            );

            InstantCreator referenceType = new InstantCreator(typeof(PropertiesOnlyModel), InstantType.Reference);
            IInstant InstantB = Instant_Creation_Compilation_Get_Set_Property_Operations_On_New_RuntimeType_Object_Instance_Test(
                referenceType,
                new PropertiesOnlyModel()
            );
        }

        [TestMethod]
        public void Instant_MemberRubric_FieldsOnlyModel_Integration_Test()
        {
            InstantCreator referenceType = new InstantCreator(
                InstantMocks.Instant_MemberRubric_FieldsOnlyModel(),
                "Instant_MemberRubric_FieldsOnlyModel_Reference"
            );
            FieldsOnlyModel fom = new FieldsOnlyModel();
            IInstant InstantA = Instant_Creation_Compilation_Get_Set_Property_Operations_On_New_RuntimeType_Object_Instance_Test(referenceType, fom);
        }

        private IInstant Instant_Creation_Compilation_Get_Set_Property_Operations_On_New_RuntimeType_Object_Instance_Test(InstantCreator str, Agreement fom)
        {
            IInstant rts = str.Create();
            fom.Kind = AgreementKind.Agree;
            rts[0] = 1;
            Assert.AreEqual(fom.Kind, rts[0]);
            rts["Id"] = 555L;
            Assert.AreNotEqual(fom.Id, rts[nameof(fom.Id)]);
            rts[nameof(fom.Comments)] = fom.Comments;
            Assert.AreEqual(fom.Comments, rts[nameof(fom.Comments)]);
            rts.Code = new Uscn(DateTime.Now.ToBinary());
            string hexTetra = rts.Code.ToString();
            Uscn ssn = new Uscn(hexTetra);
            Assert.AreEqual(ssn, rts.Code);

            for (int i = 1; i < str.Rubrics.Count; i++)
            {
                var ri = str.Rubrics[i].RubricInfo;
                if (ri.MemberType == MemberTypes.Field)
                {
                    var fi = fom.GetType().GetField(((FieldInfo)ri).Name);
                    if (fi != null)
                        rts[ri.Name] = fi.GetValue(fom);
                }
                if (ri.MemberType == MemberTypes.Property)
                {
                    var pi = fom.GetType().GetProperty(((PropertyInfo)ri).Name);
                    if (pi != null)
                    {
                        var value = pi.GetValue(fom);
                        if (value != null)
                            rts[ri.Name] = value;
                    }
                }
            }

            for (int i = 1; i < str.Rubrics.Count; i++)
            {
                var r = str.Rubrics[i].RubricInfo;
                if (r.MemberType == MemberTypes.Field)
                {
                    var fi = fom.GetType().GetField(((FieldInfo)r).Name);
                    if (fi != null)
                        Assert.AreEqual(rts[r.Name], fi.GetValue(fom));
                }
                if (r.MemberType == MemberTypes.Property && r.Name != "TypeId")
                {
                    var pi = fom.GetType().GetProperty(((PropertyInfo)r).Name);
                    if (pi != null)
                    {
                        var value = pi.GetValue(fom);
                        if (value != null)
                            Assert.AreEqual(rts[r.Name], pi.GetValue(fom));
                    }
                }
            }
            return rts;
        }

        private IInstant Instant_Creation_Compilation_Get_Set_Property_Operations_On_New_RuntimeType_Object_Instance_Test(InstantCreator str, FieldsAndPropertiesModel fom)
        {
            IInstant rts = str.Create();
            fom.Id = 202;
            rts[0] = 202L;
            Assert.AreEqual(fom.Id, rts[0]);
            rts["Id"] = 404L;
            Assert.AreNotEqual(fom.Id, rts[nameof(fom.Id)]);
            rts[nameof(fom.Name)] = fom.Name;
            Assert.AreEqual(fom.Name, rts[nameof(fom.Name)]);
            rts.Code = new Uscn(DateTime.Now.ToBinary());
            string hexTetra = rts.Code.ToString();
            Uscn ssn = new Uscn(hexTetra);
            Assert.AreEqual(ssn, rts.Code);

            for (int i = 1; i < str.Rubrics.Count; i++)
            {
                var ri = str.Rubrics[i].RubricInfo;
                if (ri.MemberType == MemberTypes.Field)
                {
                    var fi = fom.GetType().GetField(((FieldInfo)ri).Name);
                    if (fi != null)
                        rts[ri.Name] = fi.GetValue(fom);
                }
                if (ri.MemberType == MemberTypes.Property)
                {
                    var pi = fom.GetType().GetProperty(((PropertyInfo)ri).Name);
                    if (pi != null)
                        rts[ri.Name] = pi.GetValue(fom);
                }
            }

            for (int i = 1; i < str.Rubrics.Count; i++)
            {
                var r = str.Rubrics[i].RubricInfo;
                if (r.MemberType == MemberTypes.Field)
                {
                    var fi = fom.GetType().GetField(((FieldInfo)r).Name);
                    if (fi != null)
                        Assert.AreEqual(rts[r.Name], fi.GetValue(fom));
                }
                if (r.MemberType == MemberTypes.Property && r.Name != "TypeId")
                {
                    var pi = fom.GetType().GetProperty(((PropertyInfo)r).Name);
                    if (pi != null)
                        Assert.AreEqual(rts[r.Name], pi.GetValue(fom));
                }
            }
            return rts;
        }

        private IInstant Instant_Creation_Compilation_Get_Set_Property_Operations_On_New_RuntimeType_Object_Instance_Test(InstantCreator str, FieldsOnlyModel fom)
        {
            IInstant rts = str.Create();
            fom.Id = 202;
            rts["Id"] = 404L;
            Assert.AreNotEqual(fom.Id, rts[nameof(fom.Id)]);
            rts[nameof(fom.Name)] = fom.Name;
            Assert.AreEqual(fom.Name, rts[nameof(fom.Name)]);

            rts.Code = new Uscn(DateTime.Now.ToBinary());
            string hexTetra = rts.Code.ToString();
            Uscn ssn = new Uscn(hexTetra);
            Assert.AreEqual(ssn, rts.Code);

            for (int i = 1; i < str.Rubrics.Count; i++)
            {
                var r = str.Rubrics[i].RubricInfo;
                if (r.MemberType == MemberTypes.Field)
                {
                    var fi = fom.GetType().GetField(((FieldInfo)r).Name);
                    if (fi != null)
                        rts[r.Name] = fi.GetValue(fom);
                }
                if (r.MemberType == MemberTypes.Property)
                {
                    var pi = fom.GetType().GetProperty(((PropertyInfo)r).Name);
                    if (pi != null)
                        rts[r.Name] = pi.GetValue(fom);
                }
            }

            for (int i = 1; i < str.Rubrics.Count; i++)
            {
                var r = str.Rubrics[i].RubricInfo;
                if (r.MemberType == MemberTypes.Field)
                {
                    var fi = fom.GetType().GetField(((FieldInfo)r).Name);
                    if (fi != null)
                        Assert.AreEqual(rts[r.Name], fi.GetValue(fom));
                }
                if (r.MemberType == MemberTypes.Property && r.Name != "TypeId")
                {
                    var pi = fom.GetType().GetProperty(((PropertyInfo)r).Name);
                    if (pi != null)
                        Assert.AreEqual(rts[r.Name], pi.GetValue(fom));
                }
            }
            return rts;
        }

        //private void Instant_Creation_Compilation_ValueArray_Property_Operations_On_New_RuntimeType_Object_Instance_Test(InstantCreator str, IInstant Instant)
        //{
        //    IInstant rts = str.Create();
        //    object[] values = rts.ValueArray;
        //    rts.ValueArray = Instant.ValueArray;
        //    for (int i = 0; i < values.Length; i++)
        //        Assert.Equals(Instant[i], rts.ValueArray[i]);
        //}

        private IInstant Instant_Creation_Compilation_Get_Set_Property_Operations_On_New_RuntimeType_Object_Instance_Test(InstantCreator str, PropertiesOnlyModel fom)
        {
            IInstant rts = str.Create();
            fom.Id = 202;
            rts["Id"] = 404L;
            Assert.AreNotEqual(fom.Id, rts[nameof(fom.Id)]);
            rts[nameof(fom.Name)] = fom.Name;
            Assert.AreEqual(fom.Name, rts[nameof(fom.Name)]);
            rts.Code = new Uscn(DateTime.Now.ToBinary());
            string hexTetra = rts.Code.ToString();
            Uscn ssn = new Uscn(hexTetra);
            Assert.AreEqual(ssn, rts.Code);

            for (int i = 1; i < str.Rubrics.Count; i++)
            {
                var r = str.Rubrics[i].RubricInfo;
                if (r.MemberType == MemberTypes.Field)
                {
                    var fi = fom.GetType().GetField(((FieldInfo)r).Name);
                    if (fi != null)
                        rts[r.Name] = fi.GetValue(fom);
                }
                if (r.MemberType == MemberTypes.Property)
                {
                    var pi = fom.GetType().GetProperty(((PropertyInfo)r).Name);
                    if (pi != null)
                        rts[r.Name] = pi.GetValue(fom);
                }
            }

            for (int i = 1; i < str.Rubrics.Count; i++)
            {
                var r = str.Rubrics[i].RubricInfo;
                if (r.MemberType == MemberTypes.Field)
                {
                    var fi = fom.GetType().GetField(((FieldInfo)r).Name);
                    if (fi != null)
                        Assert.AreEqual(rts[r.Name], fi.GetValue(fom));
                }
                if (r.MemberType == MemberTypes.Property && r.Name != "TypeId")
                {
                    var pi = fom.GetType().GetProperty(((PropertyInfo)r).Name);
                    if (pi != null)
                        Assert.AreEqual(rts[r.Name], pi.GetValue(fom));
                }
            }
            return rts;
        }
    }
}
