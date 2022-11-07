using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Curookie.Util {
    public class ObjectPool<T> {
        const int MAX_CAPACITY = 30;
        public delegate T Func ();

        public delegate void Action (T t);

        Stack<T> buffer;
        
        /// <summary> When object create once, create, init and return the object. </summary> 
        Func createFunc;
        // FuncWithArgs createFuncWithArg;
        Func<Vector3, T> createFuncWithVector = null;
        /// <summary> Befere object put pool, reset the object. </summary> 
        Action resetFunc = null;

        /// <summary> Befere u recycle object, init the object. </summary> 
        Action initFunc = null;
        Action<T, Vector3> initFuncWithVector = null;

        int index;

        public ObjectPool (Func createFunc, Action resetFunc, Action initFunc, int size = 0, int capacity = MAX_CAPACITY) {
            if (createFunc == null) {
                return;
            }
            this.buffer = new Stack<T> ();
            this.createFunc = createFunc;
            this.resetFunc = resetFunc;
            this.initFunc = initFunc;

            this.Capacity = capacity;

            for (int i = 0; i < size; i++) {
                PutObject (createFunc ());
            }

        }

        public ObjectPool (Func<Vector3, T> createFuncWithVector, Action resetFunc, Action<T, Vector3> initFuncWithVector, Vector3? arg = null, int size = 0, int capacity = MAX_CAPACITY) {
            if (createFuncWithVector == null) {
                return;
            }
            this.buffer = new Stack<T> ();
            this.createFuncWithVector = createFuncWithVector;
            this.resetFunc = resetFunc;
            this.initFuncWithVector = initFuncWithVector;

            this.Capacity = capacity;

            for (int i = 0; i < size; i++) {
                var arg2 = arg??Vector3.zero;
                PutObject (createFuncWithVector(arg2));
            }
        }

        public int Capacity { get; private set; }
        public int Count { get { return buffer.Count; } }

        public T GetObject () {
            if (Count <= 0)
                return createFunc ();
            else {
                var obj = buffer.Pop ();
                if(initFunc != null) initFunc (obj);
                return obj;
            }
        }

        public T GetObject (Vector3 param) {
            if (Count <= 0)
                return createFuncWithVector (param);
            else {
                var obj = buffer.Pop ();
                if(initFuncWithVector != null) initFuncWithVector (obj, param);
                return obj;
            }
        }

        public void PutObject (T obj) {
            if (Count >= Capacity)
                return;

            if (resetFunc != null)
                resetFunc (obj);

            buffer.Push (obj);
        }
    }

    // 첫번째 파라미터로 클래스 생성시에 붙여줄 떄 사용, Transform 도 class니까 되는거 아닌감??
    public class ObjectPool<T, P1> where P1 : class {
        const int MAX_CAPACITY = 30;
        public delegate T Func ();
        // public delegate T FuncWithParam (P1 p1);

        public delegate void Action (T t);

        Stack<T> buffer;
        
        /// <summary> When object create once, create, init and return the object. </summary> 
        Func<P1, T> createFuncWithParam;
        /// <summary> Befere object put pool, reset the object. </summary> 
        Action resetFunc;
        /// <summary> Befere u recycle object, init the object. </summary> 
        Action<T,P1> initFuncWithParam;

        int index;

        public ObjectPool (Func<P1, T> createFunc, Action resetFunc, Action<T, P1> initFunc, int size = 0, int capacity = MAX_CAPACITY)  {
            if (createFunc == null) {
                return;
            }
            this.buffer = new Stack<T> ();
            this.createFuncWithParam = createFunc;
            this.resetFunc = resetFunc;
            this.initFuncWithParam = initFunc;

            this.Capacity = capacity;
            // var card = createFuncWithParam.Method.GetParameters()[0].GetRealObject(new System.Runtime.Serialization.StreamingContext());
            // Debug.Log(createFunc.Target);
            // Debug.Log(createFunc.Method.GetParameters()[0]);

            // Debug.Log(((BaseCard) card).baseName );

            
            for (int i = 0; i < size; i++) {
                PutObject (createFuncWithParam.Invoke(null));
            }
        }

        public int Capacity { get; private set; }
        public int Count { get { return buffer.Count; } }

        public T GetObject (P1 param) {
            if (Count <= 0)
                return createFuncWithParam (param);
            else {
                var obj = buffer.Pop ();
                if(initFuncWithParam != null) initFuncWithParam (obj, param);
                return obj;
            }
        }

        public void PutObject (T obj) {
            if (Count >= Capacity)
                return;

            if (resetFunc != null)
                resetFunc (obj);

            buffer.Push (obj);
        }
    }

    
    // 첫번째 파라미터로 클래스, 두번째 파라미터로 트랜스폼 값 생성시 넣어주기
    public class ObjectPool<T, P1, P2> where P1 : class where P2 : Transform {
        const int MAX_CAPACITY = 30;
        public delegate T Func ();
        // public delegate T FuncWithParam (P1 p1);

        public delegate void Action (T t);

        Stack<T> buffer;
        
        /// <summary> When object create once, create, init and return the object. </summary> 
        Func<P1, P2, T> createFuncWithParam;
        /// <summary> Befere object put pool, reset the object. </summary> 
        Action resetFunc;
        /// <summary> Befere u recycle object, init the object. </summary> 
        Action<T, P1, P2> initFuncWithParam;

        int index;

        public ObjectPool (Func<P1, P2, T> createFunc, Action resetFunc, Action<T, P1, P2> initFunc, int size = 0, int capacity = MAX_CAPACITY)  {
            if (createFunc == null) {
                return;
            }
            this.buffer = new Stack<T> ();
            this.createFuncWithParam = createFunc;
            this.resetFunc = resetFunc;
            this.initFuncWithParam = initFunc;

            this.Capacity = capacity;
            // var card = createFuncWithParam.Method.GetParameters()[0].GetRealObject(new System.Runtime.Serialization.StreamingContext());
            // Debug.Log(createFunc.Target);
            // Debug.Log(createFunc.Method.GetParameters()[0]);

            // Debug.Log(((BaseCard) card).baseName );

            
            for (int i = 0; i < size; i++) {
                PutObject (createFuncWithParam.Invoke(null, null));
            }
        }

        public int Capacity { get; private set; }
        public int Count { get { return buffer.Count; } }

        public T GetObject (P1 param, P2 tranform) {
            if (Count <= 0)
                return createFuncWithParam (param, tranform);
            else {
                var obj = buffer.Pop ();
                if(initFuncWithParam != null) initFuncWithParam (obj, param, tranform);
                return obj;
            }
        }

        public void PutObject (T obj) {
            if (Count >= Capacity)
                return;

            if (resetFunc != null)
                resetFunc (obj);

            buffer.Push (obj);
        }
    }
}