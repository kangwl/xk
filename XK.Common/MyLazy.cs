namespace XK.Common {
    public class MyLazy<T> where T : new() {
        private T value;
        private bool isLoaded;

        public MyLazy() {
            isLoaded = false;
        }

        public T Value {
            get {
                if (!isLoaded) {
                    value = new T();
                    isLoaded = true;
                }
                return value;
            }
        }
    }
}
