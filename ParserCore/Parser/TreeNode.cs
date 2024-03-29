﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilingPrinciples.ParserCore
{
    public class TreeNode<T>
    {
        private readonly T value;
        private readonly List<TreeNode<T>> children = new List<TreeNode<T>>();

        public TreeNode(T value)
        {
            this.value = value;
        }

        public TreeNode<T> this[int i]
        {
            get { return children[i]; }
        }

        public TreeNode<T> Parent { get; private set; }

        public T Value
        {
            get { return value; }
        }

        public IReadOnlyCollection<TreeNode<T>> Children
        {
            get { return children.AsReadOnly(); }
        }

        public IReadOnlyCollection<TreeNode<T>> Siblings
        {
            get { return Parent.Children; }
        }

        public TreeNode<T> AddChild(T value)
        {
            var node = new TreeNode<T>(value) { Parent = this };
            children.Add(node);
            return node;
        }

        public void AddChild(TreeNode<T> node)
        {
            node.Parent = this;
            children.Add(node);
        }

        public TreeNode<T>[] AddChildren(params T[] values)
        {
            return values.Select(AddChild).ToArray();
        }

        public void AddChildren(params TreeNode<T>[] nodes)
        {
            foreach (var node in nodes)
                AddChild(node);
        }

        public void InsertFirst(TreeNode<T> node)
        {
            node.Parent = this;
            children.Insert(0, node);
        }

        public bool RemoveChild(TreeNode<T> node)
        {
            return children.Remove(node);
        }

        public void Traverse(Action<TreeNode<T>> action)
        {
            action(this);
            foreach (var child in children)
                child.Traverse(action);
        }

        public IEnumerable<T> Flatten()
        {
            return new[] { Value }.Union(children.SelectMany(x => x.Flatten()));
        }
    }
}
