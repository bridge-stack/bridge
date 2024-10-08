﻿using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson;
using MongoDB.Bson.IO;

namespace Bridge.Workflow.Storage.MongoDB;

public class AssemblyQualifiedDiscriminatorConvention : IDiscriminatorConvention
{
    public string ElementName
    {
        get
        {
            return "_t";
        }
    }

    public Type GetActualType(IBsonReader bsonReader, Type nominalType)
    {
        var bookmark = bsonReader.GetBookmark();
        bsonReader.ReadStartDocument();
        string typeValue = string.Empty;
        if (bsonReader.FindElement(ElementName))
            typeValue = bsonReader.ReadString();
        else
            throw new NotSupportedException();

        bsonReader.ReturnToBookmark(bookmark);
        var result = Type.GetType(typeValue);
        return result;
    }

    public BsonValue GetDiscriminator(Type nominalType, Type actualType)
    {
        return actualType.AssemblyQualifiedName;
    }
}