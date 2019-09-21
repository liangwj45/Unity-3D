using System.Collections;
using UnityEngine;

namespace PreistDevil {
    public interface IObject {
        void SetPosition(Vector3 position);
        void SetScale(Vector3 scale);
        void Init();
    }
}