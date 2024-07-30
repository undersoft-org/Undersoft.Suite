﻿using System.Text.Json;

namespace Undersoft.SDK.Serialization;

public interface ISerializableJsonDocument
{
    JsonDocument Document { get; set; }

    string TypeName { get; set; }

    T GetStructure<T>();

    void SetDocument<T>(T structure);
}
