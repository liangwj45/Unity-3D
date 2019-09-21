using System.Collections;
using UnityEngine;

namespace PreistDevil {
    public enum ObjectType :uint {
        PREIST, DEVIL, BOAT, COAST, WATER
    }

    public interface IObject {
        void SetPosition(Vector3 position);
        void SetScale(Vector3 scale);
        ObjectType GetObjectType();
    }
}