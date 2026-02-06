// SPDX-FileCopyrightText: 2025 Goob Station contributors
//
// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Content.ModuleManager;

[AttributeUsage(AttributeTargets.Assembly)]
public sealed class ContentModuleAttribute(ModuleType type) : Attribute
{
    public ModuleType Type { get; } = type;
}

public enum ModuleType
{
    Client,
    Server,
    Shared,
    Common,
}
